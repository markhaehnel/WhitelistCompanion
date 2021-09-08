using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace WhitelistCompanion.Installer
{
    interface IInstaller
    {
        void Install(IServiceCollection services, IConfiguration configuration);
    }

}
