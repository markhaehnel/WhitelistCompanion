using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WhitelistCompanion.Utils;

namespace WhitelistCompanion.Installer
{
    public class FilterInstaller : IInstaller
    {
        public void Install(IServiceCollection services, IConfiguration configuration)
        {
            if (configuration is null) throw new ArgumentNullException(nameof(configuration));

            services.AddControllers(options => options.Filters.Add<ExceptionFilter>());
        }
    }

}
