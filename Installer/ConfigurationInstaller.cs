using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WhitelistCompanion.Configuration;

namespace WhitelistCompanion.Installer
{
    public class ConfigurationInstaller : IInstaller
    {
        public void Install(IServiceCollection services, IConfiguration configuration)
        {
            if (configuration is null) throw new ArgumentNullException(nameof(configuration));

            services.AddOptions<ApiConfiguration>()
                .Bind(configuration.GetSection(ApiConfiguration.Section))
                .ValidateDataAnnotations();
            services.AddOptions<MinecraftConfiguration>()
                .Bind(configuration.GetSection(MinecraftConfiguration.Section))
                .ValidateDataAnnotations();
            services.AddOptions<MicrosoftAuthConfiguration>()
                .Bind(configuration.GetSection(MicrosoftAuthConfiguration.Section))
                .ValidateDataAnnotations();
        }
    }

}
