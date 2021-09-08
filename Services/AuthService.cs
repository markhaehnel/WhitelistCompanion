using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using WhitelistCompanion.Configuration;
using WhitelistCompanion.Models.Auth;

namespace WhitelistCompanion.Services
{
    public class AuthService
    {
        private readonly ILogger<AuthService> _logger;

        private readonly MicrosoftAuthConfiguration _config;
        private readonly HttpClient _httpClient;

        public AuthService(ILogger<AuthService> logger, IOptions<MicrosoftAuthConfiguration> config, IHttpClientFactory httpClientFactory)
        {
            if (config is null) throw new ArgumentNullException(nameof(config));
            if (httpClientFactory is null) throw new ArgumentNullException(nameof(httpClientFactory));

            _logger = logger;
            _config = config.Value;
            _httpClient = httpClientFactory.CreateClient(Constants.MicrosoftAuthApiClientName);
        }

        private Dictionary<string, string> GetDefaultParams()
        {
            return new Dictionary<string, string> {
                { "client_id", _config.ClientId },
                { "client_secret", _config.ClientSecret },
                { "redirect_uri", _config.RedirectUri.ToString() }
            };
        }

        private string BuildQuery(string path, Dictionary<string, string> queryParams)
        {
            var mergedParams = GetDefaultParams().Concat(queryParams);
            var mergedParamsString = string.Join("&", mergedParams.Select(kvp => $"{kvp.Key}={kvp.Value}"));
            return $"{path}?{mergedParamsString}";
        }

        public Uri GetAuthorizeUri(string state)
        {
            var queryParams = new Dictionary<string, string> {
                { "scope", "XboxLive.signin" },
                { "response_type", "code" },
                { "state", state }
            };
            return new Uri($"{_httpClient.BaseAddress}{BuildQuery("authorize", queryParams)}");
        }

        public async Task<string> ExchangeCodeForTokenAsync(string code)
        {
            var queryParams = new Dictionary<string, string>()
            {
                { "grant_type", "authorization_code" },
                { "code", code }
            };

            using var requestMessage = new HttpRequestMessage(HttpMethod.Post, "token")
            {
                Content = new FormUrlEncodedContent(GetDefaultParams().Concat(queryParams))
            };

            var rawResponse = await _httpClient.SendAsync(requestMessage);
            rawResponse.EnsureSuccessStatusCode();

            var tokenResponse = await rawResponse.Content.ReadFromJsonAsync<TokenResponse>();

            return tokenResponse.AccessToken;
        }
    }
}
