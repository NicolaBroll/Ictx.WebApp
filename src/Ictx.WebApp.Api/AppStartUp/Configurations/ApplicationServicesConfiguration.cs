using Ictx.WebApp.Application.BO;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Ictx.WebApp.Api.AppStartUp.Configurations
{
    public static class ApplicationServicesConfiguration
    {
        /// <summary>Configura la Dependency Injection aggiungendo tutti i servizi che utilizza l'app.</summary>
        public static IServiceCollection ConfigureApplicationServices(this IServiceCollection services)
        {
            // Services.
            services.TryAddScoped<DipendenteBO>();
            services.TryAddScoped<MailBO>();

            return services;
        }
    }
}
