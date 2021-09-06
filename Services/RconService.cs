using System;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using CoreRCON;
using CoreRCON.Parsers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using WhitelistCompanion.Models.Rcon;

namespace WhitelistCompanion.Services
{
    public class RconService
    {
        private readonly ILogger<RconService> _logger;
        private readonly IConfiguration _config;

        private RCON _rcon;
        private bool _rconGuard;
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

            _logger.LogDebug($"Using {_host}:{_port} for RCON connections");
        }

        private async Task<T> GuardedSendCommandAsync<T>(string command) where T : class, IParseable, new()
        {
            while (_rconGuard) await Task.Delay(100);

            _rconGuard = true;

            try
            {
                var rcon = await GetRconClientAsync();
                var result = await rcon.SendCommandAsync<T>(command);

                return result;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                _rconGuard = false;
            }
        }

        private async Task<RCON> GetRconClientAsync()
        {
            if (_rcon != null)
            {
                await _rcon.ConnectAsync();
                return _rcon;
            }

            var ip = (await Dns.GetHostAddressesAsync(_host)).First();
            _rcon = new RCON(ip, _port, _password);
            await _rcon.ConnectAsync();
            _rcon.OnDisconnected += () =>
            {
                _logger.LogInformation("RCON disconnected. Recreating on next access.");
                _rcon = null;
            };

            return _rcon;
        }

        public async Task<WhitelistAddCommandResponse> AddToWhitelist(string username)
        {
            if (string.IsNullOrEmpty(username)) throw new ArgumentNullException(nameof(username));
            var result = await GuardedSendCommandAsync<WhitelistAddCommandResponse>($"whitelist add {username}");
            return result;
        }

        public async Task<WhitelistListCommandResponse> GetWhitelist()
        {
            var result = await GuardedSendCommandAsync<WhitelistListCommandResponse>("whitelist list");
            return result;
        }

        public async Task<UserListCommandResponse> GetUserList()
        {
            var result = await GuardedSendCommandAsync<UserListCommandResponse>("list");
            return result;
        }
    }
}
