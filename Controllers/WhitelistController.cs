using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WhitelistCompanion.Attributes;
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
        public async Task<IEnumerable<string>> GetWhitelistAsync()
        {
            return await _rconService.GetWhitelist();
        }

        [HttpPost]
        public async Task<bool> AddUsernameToWhitelist(string username)
        {
            return await _rconService.AddToWhitelist(username);
        }
    }
}
