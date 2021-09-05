using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;
using CoreRCON;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using WhitelistCompanion.Models;
using WhitelistCompanion.Models.Rcon;

namespace WhitelistCompanion.Services
{
    public class RconService
    {
        private readonly ILogger<RconService> _logger;
        private readonly IConfiguration _config;

        private readonly string _host;
        private readonly ushort _port;
        private readonly string _password;

        public RconService(ILogger<RconService> logger, IConfiguration config)
        {
            _logger = logger;
            _config = config;

            _host = _config.GetValue<string>("MC_HOST");
            _port = _config.GetValue<ushort>("MC_PORT");
            _password = _config.GetValue<string>("MC_PASSWORD");

            _logger.LogInformation($"Using {_host}:{_port} for RCON connections");
        }
        private async Task<RCON> GetRconClientAsync()
        {
            var ip = (await Dns.GetHostAddressesAsync(_host)).First();
            var rcon = new RCON(ip, _port, _password);
            await rcon.ConnectAsync();

            return rcon;
        }

        public async Task<WhitelistAddCommandResponse> AddToWhitelist(string username)
        {
            if (string.IsNullOrEmpty(username)) throw new ArgumentNullException(nameof(username));

            var rcon = await GetRconClientAsync();
            var result = await rcon.SendCommandAsync<WhitelistAddCommandResponse>($"whitelist add {username}");

            return result;
        }

        public async Task<WhitelistListCommandResponse> GetWhitelist()
        {
            var rcon = await GetRconClientAsync();
            var result = await rcon.SendCommandAsync<WhitelistListCommandResponse>("whitelist list");

            return result;
        }
    }
}
