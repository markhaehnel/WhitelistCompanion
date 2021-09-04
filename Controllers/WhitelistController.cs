using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WhitelistCompanion.Attributes;

namespace WhitelistCompanion.Controllers
{
    [ApiController]
    [ApiKeyAuthorization]
    [Route("[controller]")]
    public class WhitelistController : ControllerBase
    {
        private readonly ILogger<WhitelistController> _logger;

        public WhitelistController(ILogger<WhitelistController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public async Task<string> PostAsync(string username)
        {
            return username;
        }
    }
}
