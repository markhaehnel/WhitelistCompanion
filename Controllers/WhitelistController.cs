using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WhitelistCompanion.Attributes;
using WhitelistCompanion.Models;
using WhitelistCompanion.Services;

namespace WhitelistCompanion.Controllers
{
    [ApiController]
    [ApiKeyAuthorization]
    [Route("[controller]")]
    public class WhitelistController : ControllerBase
    {
        private readonly ILogger<WhitelistController> _logger;
        private readonly RconService _rconService;

        public WhitelistController(ILogger<WhitelistController> logger, RconService rconService)
        {
            _logger = logger;
            _rconService = rconService;
        }

        [HttpGet]
        public async Task<ApiResponse<WhitelistListResponse>> GetWhitelistAsync()
        {
            var result = await _rconService.GetWhitelist();

            return new ApiResponse<WhitelistListResponse>
            {
                Data = new WhitelistListResponse
                {
                    Users = result.Users
                }
            };

        }

        [HttpPost]
        public async Task<ApiResponse<WhitelistAddResponse>> AddToWhitelistAsync([FromBody] WhitelistAddRequest whitelistAddRequest)
        {
            if (whitelistAddRequest is null) throw new ArgumentNullException(nameof(whitelistAddRequest));

            var result = await _rconService.AddToWhitelist(whitelistAddRequest.User);

            return new ApiResponse<WhitelistAddResponse>
            {
                Data = new WhitelistAddResponse
                {
                    Success = result.Success,
                    User = result.User
                }
            };
        }
    }
}
