using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WhitelistCompanion.Configuration;
using WhitelistCompanion.Extensions;

namespace WhitelistCompanion.Installer
{
    public class ConfigurationInstaller : IInstaller
    {
        public void Install(IServiceCollection services, IConfiguration configuration)
        {
            if (configuration is null) throw new ArgumentNullException(nameof(configuration));

            services.ConfigureAndValidate<ApiConfiguration>(ApiConfiguration.Section, configuration);
            services.ConfigureAndValidate<MicrosoftAuthConfiguration>(MicrosoftAuthConfiguration.Section, configuration);
            services.ConfigureAndValidate<MinecraftConfiguration>(MinecraftConfiguration.Section, configuration);
        }
    }
}
