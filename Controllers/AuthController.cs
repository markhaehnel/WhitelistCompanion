using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WhitelistCompanion.Attributes;
using WhitelistCompanion.Services;

namespace WhitelistCompanion.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly ILogger<AuthController> _logger;
        private readonly MicrosoftAuthService _msAuthService;
        private readonly XboxLiveService _xblService;
        private readonly XstsService _xstsService;
        private readonly MinecraftService _mcService;

        public AuthController(
            ILogger<AuthController> logger,
            MicrosoftAuthService msAuthService,
            XboxLiveService xblService,
            XstsService xstsService,
            MinecraftService mcService)
        {
            _logger = logger;
            _msAuthService = msAuthService;
            _xblService = xblService;
            _xstsService = xstsService;
            _mcService = mcService;
        }

        [ApiKeyAuthorization]
        [HttpGet()]
        public IActionResult Auth([FromQuery] string state)
        {
            return Redirect(_msAuthService.GetAuthorizeUri(state).ToString());
        }

        [HttpGet("callback")]
        public async Task<IActionResult> AuthCallbackAsync([FromQuery] string code, [FromQuery] string state)
        {
            var msAuthResult = await _msAuthService.ExchangeCodeForTokenAsync(code);
            var xblAuthResult = await _xblService.Authenticate(msAuthResult.AccessToken);
            var xstsAuthResult = await _xstsService.Authorize(xblAuthResult.Token);
            var mcAuthResult = await _mcService.AuthenticateWithXbox(
                xstsAuthResult.DisplayClaims.Xui.First().Uhs,
                xstsAuthResult.Token
            );
            var mcProfileResult = await _mcService.GetProfile(mcAuthResult.AccessToken);

            return Redirect($"/?secret={state}&addUser={mcProfileResult.Name}");
        }
    }
}
