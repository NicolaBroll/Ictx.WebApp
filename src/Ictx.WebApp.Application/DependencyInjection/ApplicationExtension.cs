using Ictx.WebApp.Application.BO;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Ictx.WebApp.Application.DependencyInjection
{
    public static class ApplicationExtension
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            // Services.
            services.TryAddScoped<DipendenteBO>();

            return services;
        }
    }
}
