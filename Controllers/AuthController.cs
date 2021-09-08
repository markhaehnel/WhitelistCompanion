using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WhitelistCompanion.Services;

namespace WhitelistCompanion.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly ILogger<AuthController> _logger;
        private readonly AuthService _authService;

        public AuthController(ILogger<AuthController> logger, AuthService authService)
        {
            _logger = logger;
            _authService = authService;
        }

        [HttpGet()]
        public IActionResult Auth([FromQuery] string state)
        {
            return Redirect(_authService.GetAuthorizeUri(state).ToString());
        }

        [HttpGet("callback")]
        public async Task<IActionResult> AuthCallbackAsync([FromQuery] string code, [FromQuery] string state)
        {
            var result = await _authService.ExchangeCodeForTokenAsync(code);
            return Redirect($"/?secret={state}");
        }
    }
}
