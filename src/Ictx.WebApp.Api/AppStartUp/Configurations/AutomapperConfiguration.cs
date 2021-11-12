using System.Reflection;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;

namespace Ictx.WebApp.Api.AppStartUp.Configurations;

public static class AutomapperConfiguration
{
    /// <summary>Configura la Dependency Injection aggiungendo la configurazione letta dall'appsettings.json.</summary>
    public static IServiceCollection ConfigureAutomapper(this IServiceCollection services)
    {
        services.AddAutoMapper(
            // Questa configurazione è necessaria in quanto automapper in presenza di un costruttore con i parametri, non riesce a mappare correttamente l'entità
            cfg => cfg.DisableConstructorMapping(),
            new List<Assembly>() { Assembly.GetExecutingAssembly()            
        });

        return services;
    }
}