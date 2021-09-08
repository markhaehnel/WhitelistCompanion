using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WhitelistCompanion.Configuration;
using WhitelistCompanion.Services;

namespace WhitelistCompanion
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddOptions<ApiConfiguration>()
                .Bind(Configuration.GetSection(ApiConfiguration.Section))
                .ValidateDataAnnotations();
            services.AddOptions<MinecraftConfiguration>()
                .Bind(Configuration.GetSection(MinecraftConfiguration.Section))
                .ValidateDataAnnotations();
            services.AddOptions<MicrosoftAuthConfiguration>()
                .Bind(Configuration.GetSection(MicrosoftAuthConfiguration.Section))
                .ValidateDataAnnotations();

            services.AddControllersWithViews();

            // In production, the React files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "Web/dist";
            });

            services.AddSingleton<RconService>();
            services.AddSingleton<AuthService>();

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

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseStaticFiles();
            app.UseSpaStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
            });

            app.UseSpa(spa =>
            {
                if (env.IsDevelopment())
                {
                    spa.Options.SourcePath = "Web";
                    spa.Options.DevServerPort = 3000;
                    spa.UseReactDevelopmentServer("dev");
                }
            });
        }
    }
}
