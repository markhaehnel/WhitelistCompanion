using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using CoreRCON;
using CoreRCON.Parsers;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Nito.AsyncEx;
using WhitelistCompanion.Configuration;
using WhitelistCompanion.Models.Rcon;

namespace WhitelistCompanion.Services
{
    public class RconService : IDisposable
    {
        private readonly ILogger<RconService> _logger;
        private readonly MinecraftConfiguration _config;

        private RCON _rcon;
        private readonly AsyncLock _rconLock = new();

        private bool _disposed;

        public RconService(ILogger<RconService> logger, IOptions<MinecraftConfiguration> options)
        {
            if (options is null) throw new ArgumentNullException(nameof(options));

            _logger = logger;
            _config = options.Value;

            _logger.LogInformation($"Using {_config.Hostname}:{_config.Port} for RCON connections.");
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

        private async Task<T> GuardedSendCommandAsync<T>(string command) where T : class, IParseable, new()
        {
            // Attempt to take the lock only for 3 seconds.
            using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(3));

            using (await _rconLock.LockAsync(cts.Token))
            {
                var rcon = await GetRconClientAsync();
                _logger.LogDebug($"Running RCON command: {command}");
                var result = await rcon.SendCommandAsync<T>(command);

                return result;
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031")]
        private async Task<RCON> GetRconClientAsync()
        {
            try
            {
                if (_rcon != null)
                {
                    await _rcon.ConnectAsync();
                    return _rcon;
                }

                _logger.LogDebug($"Creating RCON client.");
                var ip = (await Dns.GetHostAddressesAsync(_config.Hostname)).First();
                _rcon = new RCON(ip, _config.Port, _config.Password);

                await _rcon.ConnectAsync();

                _rcon.OnDisconnected += () =>
                {
                    _logger.LogDebug("RCON disconnected. Recreating RCON client on next access.");
                    _rcon = null;
                };

                return _rcon;
            }
            catch (Exception ex)
            {
                _logger.LogDebug($"Catched {ex.GetType()}. Disposing client.");
                try { _rcon?.Dispose(); } catch { };
                _rcon = null;

                throw new RconUnavailableException("RCON is unavailable", ex);
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed) return;

            if (disposing) _rcon?.Dispose();

            _disposed = true;
        }
    }

    public class RconUnavailableException : Exception
    {
        public RconUnavailableException() : base() { }
        public RconUnavailableException(string message) : base(message) { }
        public RconUnavailableException(string message, Exception innerException) : base(message, innerException) { }
    }
}
