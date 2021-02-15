using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Reflection;

namespace Ictx.WebApp.Api.AppStartUp
{
    /// <summary>Configura la Dependency Injection relativamente alle classi di mappatura di AutoMapper.</summary>
    public class AutomapperInstaller : IInstaller
    {
        /// <summary>
        /// Aggiunge alla Dependency Injection tutte le classi che estendono Profile e si trovano nello stesso assembly
        /// di PlaceholderProfile e PlaceholderDtoProfile
        /// </summary>	
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            // Automapper.
            services.AddAutoMapper(

            // Questa configurazione è necessaria in quanto automapper in presenza di un costruttore con i parametri, non riesce a mappare correttamente l'entità
            cfg => cfg.DisableConstructorMapping(),
            new List<Assembly>() {
                            typeof(PlaceholderProfile).Assembly,
                            typeof(PlaceholderDTOProfile).Assembly
            }
        );
        }
    }

    internal class PlaceholderDTOProfile : Profile
    {
    }

    internal class PlaceholderProfile : Profile
    {
    }
}
