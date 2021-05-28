using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Ictx.WebApp.Infrastructure.Common;
using Ictx.WebApp.Api.Common;

namespace Ictx.WebApp.Api.AppStartUp.Configurations
{
    public static class ApplicationSettingsConfiguration
    {
        /// <summary>Configura la Dependency Injection aggiungendo la configurazione letta dall'appsettings.json.</summary>
        public static IServiceCollection ConfigureApplicationSettings(this IServiceCollection services, IConfiguration _configuration)
        {
            // Mail.
            services.Configure<MailSettings>(_configuration.GetSection(nameof(MailSettings)));
            services.AddSingleton<IMailSettings>(sp => sp.GetRequiredService<IOptions<MailSettings>>().Value);

            // Auth.
            services.Configure<AuthSettings>(_configuration.GetSection(nameof(AuthSettings)));
            services.AddSingleton<IAuthSettings>(sp => sp.GetRequiredService<IOptions<AuthSettings>>().Value);

            return services;
        }
    }
}
