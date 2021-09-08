using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace WhitelistCompanion.Installer
{
    public class HttpClientInstaller : IInstaller
    {
        public void Install(IServiceCollection services, IConfiguration configuration)
        {
            if (configuration is null) throw new ArgumentNullException(nameof(configuration));

            services.AddHttpClient(Constants.MicrosoftAuthApiClientName, client =>
            {
                client.BaseAddress = new Uri("https://login.microsoftonline.com/consumers/oauth2/v2.0/");
            });
            services.AddHttpClient(Constants.XblApiClientName, client =>
            {
                client.BaseAddress = new Uri("https://user.auth.xboxlive.com/");
            });
            services.AddHttpClient(Constants.XstsApiClientName, client =>
            {
                client.BaseAddress = new Uri("https://xsts.auth.xboxlive.com/xsts/");
            });
            services.AddHttpClient(Constants.MinecraftApiClientName, client =>
            {
                client.BaseAddress = new Uri("https://api.minecraftservices.com/");
            });

        }
    }

}
