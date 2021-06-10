using Microsoft.Extensions.DependencyInjection;
using Ictx.WebApp.Application.Services;
using Ictx.WebApp.Infrastructure.Services;
using Ictx.WebApp.Application.AppUnitOfWork;
using Ictx.WebApp.Infrastructure.UnitOfWork;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Ictx.WebApp.Application.Repositories;
using Ictx.WebApp.Infrastructure.Repositories;

namespace Ictx.WebApp.Infrastructure.AppStartUp.Configurations
{
    public static class InfrastructureServicesConfiguration
    {
        public static IServiceCollection ConfigureInfrastructureServices(this IServiceCollection services)
        {
            // Services.
            services.TryAddSingleton<IDateTimeService, DateTimeService>();

            // Unit of work.
            services.TryAddScoped<IAppUnitOfWork, AppUnitOfWork>();

            // Repository.
            services.TryAddScoped<IDipendenteRepository, DipendenteRepository>();

            return services;
        }
    }
}
