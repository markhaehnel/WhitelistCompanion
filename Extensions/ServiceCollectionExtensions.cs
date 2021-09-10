using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WhitelistCompanion.Extensions.Validation;
using WhitelistCompanion.Installer;

namespace WhitelistCompanion.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void InstallServicesInAssembly(this IServiceCollection services, IConfiguration configuration)
        {
            var installers = typeof(Startup).Assembly.ExportedTypes.Where(x =>
              typeof(IInstaller).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract).Select(Activator.CreateInstance).Cast<IInstaller>().ToList();

            installers.ForEach(installer => installer.Install(services, configuration));
        }

        public static void ConfigureAndValidate<TOptions>(this IServiceCollection services, string sectionName, IConfiguration configuration) where TOptions : class
        {
            if (configuration is null) throw new ArgumentNullException(nameof(configuration));

            var section = configuration.GetSection(sectionName);
            try
            {
                section.Get<TOptions>().ValidateAndThrow();

                services.Configure<TOptions>(section);
            }
            catch (ValidationException ex)
            {
                var fieldPath = $"{sectionName}:{ex.ValidationResult.MemberNames.First()}";
                throw new ValidationException($"Validation of configuration option {fieldPath} failed. {ex.ValidationResult.ErrorMessage}", ex);
            }
        }
    }
}
