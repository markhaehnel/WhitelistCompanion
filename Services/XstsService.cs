using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using WhitelistCompanion.Models.Auth.Xsts;
using WhitelistCompanion.Utils;

namespace WhitelistCompanion.Services
{
    public class XstsService
    {
        private readonly ILogger<XstsService> _logger;

        private readonly HttpClient _httpClient;

        public XstsService(ILogger<XstsService> logger, IHttpClientFactory httpClientFactory)
        {
            if (httpClientFactory is null) throw new ArgumentNullException(nameof(httpClientFactory));

            _logger = logger;
            _httpClient = httpClientFactory.CreateClient(Constants.XstsApiClientName);
        }

        public async Task<XstsAuthTokenResponse> Authorize(string xblToken)
        {
            var requestData = new XstsAuthTokenRequest(
                Properties: new XstsAuthTokenRequestProperties(
                    SandboxId: "RETAIL",
                    UserTokens: new string[] { xblToken }
                ),
                RelyingParty: "rp://api.minecraftservices.com/",
                TokenType: "JWT"
            );

            // This endpoint is picky. We need to precisely set Accept and Content-Type
            // headers to "application/json" without Encodings
            using var requestMessage = new HttpRequestMessage(HttpMethod.Post, "authorize")
            {
                Content = new StringContent(
                    JsonSerializer.Serialize(requestData),
                    null,
                    "application/json"
                )
            };
            requestMessage.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var rawResponse = await _httpClient.SendAsync(requestMessage);
            rawResponse.EnsureSuccessStatusCode();

            var tokenResponse = await rawResponse.Content.ReadFromJsonAsync<XstsAuthTokenResponse>();

            return tokenResponse;
        }
    }
}
