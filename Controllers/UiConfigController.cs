using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using WhitelistCompanion.Attributes;
using WhitelistCompanion.Configuration;
using WhitelistCompanion.Models;

namespace WhitelistCompanion.Controllers
{
    [ApiController]
    [ApiKeyAuthorization]
    [Route("[controller]")]
    public class UiConfigController : ControllerBase
    {
        private readonly ILogger<UiConfigController> _logger;
        private readonly UiConfiguration _config;

        public UiConfigController(ILogger<UiConfigController> logger, IOptions<UiConfiguration> config)
        {
            if (config is null) throw new ArgumentNullException(nameof(config));

            _logger = logger;
            _config = config.Value;
        }

        [HttpGet]
        [SuppressMessage("Microsoft.Usage", "CA1024")]
        public ApiResponse<UiConfigResponse> GetUiConfig()
        {
            return new ApiResponse<UiConfigResponse>
            {
                Data = new UiConfigResponse
                {
                    ServerAddress = _config.ServerAddress,
                    MapUri = _config.MapUri,
                    MapPreviewUri = string.IsNullOrEmpty(_config.MapPreviewUri) ? _config.MapUri : _config.MapPreviewUri
                }
            };
        }
    }
}
