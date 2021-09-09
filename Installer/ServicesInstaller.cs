using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WhitelistCompanion.Services;

namespace WhitelistCompanion.Installer
{
    public class ServicesInstaller : IInstaller
    {
        public void Install(IServiceCollection services, IConfiguration configuration)
        {
            if (configuration is null) throw new ArgumentNullException(nameof(configuration));

            services.AddSingleton<RconService>();
            services.AddScoped<MicrosoftAuthService>();
            services.AddScoped<XboxLiveService>();
            services.AddScoped<XstsService>();
            services.AddScoped<MinecraftService>();
        }
    }

}
