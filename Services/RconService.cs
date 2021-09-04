using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using CoreRCON;

namespace WhitelistCompanion.Services
{
    public class RconService
    {
        private static async Task<RCON> GetRconClientAsync()
        {
            var ip = (await Dns.GetHostAddressesAsync("localhost")).First();
            var rcon = new RCON(ip, 27015, "YoloSwag1337");
            await rcon.ConnectAsync();

            return rcon;
        }

        public async Task<bool> AddToWhitelist(string username)
        {
            if (string.IsNullOrEmpty(username)) return false;

            var rcon = await GetRconClientAsync();
            var result = await rcon.SendCommandAsync($"whitelist add {username}");

            if (string.IsNullOrEmpty(result)) return false;

            return true;
        }

        public async Task<IEnumerable<string>> GetWhitelist()
        {
            var rcon = await GetRconClientAsync();
            var result = await rcon.SendCommandAsync("whitelist list");

            if (string.IsNullOrEmpty(result)) throw new Exception("Whitelist result is empty");

            var usernames = result.Split(":")[0].Split(",");

            return usernames;
        }
    }
}
