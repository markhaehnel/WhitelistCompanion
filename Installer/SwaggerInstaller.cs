using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace WhitelistCompanion.Installer
{
    public class SwaggerInstaller : IInstaller
    {
        public void Install(IServiceCollection services, IConfiguration configuration)
        {
            if (configuration is null) throw new ArgumentNullException(nameof(configuration));

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Whitelist Companion API", Version = "v1" });
                var scheme = new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.ApiKey,
                    In = ParameterLocation.Header,
                    Name = "ApiKey"
                };
                c.AddSecurityDefinition("ApiKey", scheme);
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme {
                            Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "ApiKey" }
                        },
                        Array.Empty<string>()
                    }
                });
            });
        }
    }

}
