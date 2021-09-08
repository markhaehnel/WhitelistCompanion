using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace WhitelistCompanion.Installer
{
    public class MvcInstaller : IInstaller
    {
        public void Install(IServiceCollection services, IConfiguration configuration)
        {
            if (configuration is null) throw new ArgumentNullException(nameof(configuration));

            services.AddControllersWithViews();
            services.AddSpaStaticFiles(configuration => configuration.RootPath = "Web/dist");
        }
    }

}
