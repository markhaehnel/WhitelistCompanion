using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using WhitelistCompanion.Models.Auth.XboxLive;
using WhitelistCompanion.Utils;

namespace WhitelistCompanion.Services
{
    public class XboxLiveService
    {
        private readonly ILogger<XboxLiveService> _logger;

        private readonly HttpClient _httpClient;

        public XboxLiveService(ILogger<XboxLiveService> logger, IHttpClientFactory httpClientFactory)
        {
            if (httpClientFactory is null) throw new ArgumentNullException(nameof(httpClientFactory));

            _logger = logger;
            _httpClient = httpClientFactory.CreateClient(Constants.XboxLiveApiClientName);
        }

        public async Task<XboxLiveAuthTokenResponse> Authenticate(string accessToken)
        {
            var requestData = new XboxLiveAuthTokenRequest(
                Properties: new XboxLiveAuthTokenRequestProperties(
                    AuthMethod: "RPS",
                    SiteName: "user.auth.xboxlive.com",
                    RpsTicket: $"d={accessToken}"
                ),
                RelyingParty: "http://auth.xboxlive.com",
                TokenType: "JWT"
            );

            // This endpoint is picky. We need to precisely set Accept and Content-Type
            // headers to "application/json" without Encodings
            using var requestMessage = new HttpRequestMessage(HttpMethod.Post, "user/authenticate")
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

            var tokenResponse = await rawResponse.Content.ReadFromJsonAsync<XboxLiveAuthTokenResponse>();

            return tokenResponse;
        }
    }
}
