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
    public class UserListController : ControllerBase
    {
        private readonly ILogger<UserListController> _logger;
        private readonly RconService _rconService;

        public UserListController(ILogger<UserListController> logger, RconService rconService)
        {
            _logger = logger;
            _rconService = rconService;
        }

        [HttpGet]
        public async Task<ApiResponse<UserListResponse>> GetUserListAsync()
        {
            var result = await _rconService.GetUserList();

            return new ApiResponse<UserListResponse>
            {
                Data = new UserListResponse
                {
                    CurrentUserCount = result.CurrentUserCount,
                    MaxUserCount = result.MaxUserCount,
                    Users = result.Users
                }
            };

        }
    }
}
