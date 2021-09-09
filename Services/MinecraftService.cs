using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using WhitelistCompanion.Models.Auth.Minecraft;
using WhitelistCompanion.Utils;

namespace WhitelistCompanion.Services
{
    public class MinecraftService
    {
        private readonly ILogger<MinecraftService> _logger;

        private readonly HttpClient _httpClient;

        public MinecraftService(ILogger<MinecraftService> logger, IHttpClientFactory httpClientFactory)
        {
            if (httpClientFactory is null) throw new ArgumentNullException(nameof(httpClientFactory));

            _logger = logger;
            _httpClient = httpClientFactory.CreateClient(Constants.MinecraftApiClientName);
        }

        public async Task<MinecraftAuthTokenResponse> AuthenticateWithXbox(string userHash, string xstsToken)
        {
            var requestData = new
            {
                identityToken = $"XBL3.0 x={userHash};{xstsToken}"
            };

            var rawResponse = await _httpClient.PostAsJsonAsync("authentication/login_with_xbox", requestData);
            rawResponse.EnsureSuccessStatusCode();

            var tokenResponse = await rawResponse.Content.ReadFromJsonAsync<MinecraftAuthTokenResponse>();

            return tokenResponse;
        }

        public async Task<MinecraftProfileResponse> GetProfile(string mcToken)
        {
            // We can set the default header here, because the service is scoped to the request context
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", mcToken);

            // Ensure minecraft entitlements
            var entitlementResponse = await _httpClient.GetFromJsonAsync<MinecraftEntitlementsResponse>("entitlements/mcstore");
            if (entitlementResponse is null) throw new HttpRequestException("The HTTP response is unsuccessful");
            if (!entitlementResponse.Items.Any()) throw new MinecraftEntitlementMissingException("Minecraft entitlement is missing");

            var profileResponse = await _httpClient.GetFromJsonAsync<MinecraftProfileResponse>("minecraft/profile");
            if (profileResponse is null) throw new HttpRequestException("The HTTP response is unsuccessful");

            return profileResponse;
        }
    }

    public class MinecraftEntitlementMissingException : Exception
    {
        public MinecraftEntitlementMissingException() : base() { }
        public MinecraftEntitlementMissingException(string message) : base(message) { }
        public MinecraftEntitlementMissingException(string message, Exception innerException) : base(message, innerException) { }
    }
}
