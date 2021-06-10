using Microsoft.Extensions.DependencyInjection;
using Ictx.WebApp.Application.Services;
using Ictx.WebApp.Infrastructure.Services;
using Ictx.WebApp.Application.UnitOfWork;
using Ictx.WebApp.Infrastructure.UnitOfWork;
using Microsoft.Extensions.DependencyInjection.Extensions;

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
            services.TryAddScoped<IBackgroundServiceUnitOfWork, BackgroundServiceUnitOfWork>();

            return services;
        }
    }
}
