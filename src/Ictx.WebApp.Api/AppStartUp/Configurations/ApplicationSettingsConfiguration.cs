using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Ictx.WebApp.Api.Common;

namespace Ictx.WebApp.Api.AppStartUp.Configurations
{
    public static class ApplicationSettingsConfiguration
    {
        /// <summary>Configura la Dependency Injection aggiungendo la configurazione letta dall'appsettings.json.</summary>
        public static IServiceCollection ConfigureApplicationSettings(this IServiceCollection services, IConfiguration _configuration)
        {
            services.Configure<ApplicationSettings>(_configuration.GetSection(nameof(ApplicationSettings)));
            services.AddSingleton<IApplicationSettings>(sp => sp.GetRequiredService<IOptions<ApplicationSettings>>().Value);

            return services;
        }
    }
}
