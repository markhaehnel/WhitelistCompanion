using System.Linq;
using System.Linq;
using System.Net;
using CoreRCON;
using CoreRCON.Parsers.Standard;

namespace WhitelistCompanion.Services
{
    public class RconService
    {
        private RCON _rcon;

        public RconService()
        {
            var ip = (await Dns.GetHostAddressesAsync("localhost")).First();
            var rcon = new RCON(IPAddress.Parse("10.0.0.1"), 27015, "secret-password");
        }
    }
}
