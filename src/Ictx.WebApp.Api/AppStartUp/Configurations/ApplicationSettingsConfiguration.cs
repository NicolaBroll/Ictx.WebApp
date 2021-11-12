using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Ictx.WebApp.Api.Common;

namespace Ictx.WebApp.Api.AppStartUp.Configurations;

public static class ApplicationSettingsConfiguration
{
    /// <summary>Configura la Dependency Injection aggiungendo la configurazione letta dall'appsettings.json.</summary>
    public static IServiceCollection ConfigureApplicationSettings(this IServiceCollection services, IConfiguration _configuration)
    {
        // Auth.
        services.Configure<AuthSettings>(_configuration.GetSection(nameof(AuthSettings)));
        services.AddSingleton<IAuthSettings>(sp => sp.GetRequiredService<IOptions<AuthSettings>>().Value);

        return services;
    }
}
