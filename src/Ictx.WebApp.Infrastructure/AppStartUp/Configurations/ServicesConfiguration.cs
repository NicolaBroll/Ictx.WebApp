using Microsoft.Extensions.DependencyInjection;
using Ictx.WebApp.Application.Services;
using Ictx.WebApp.Infrastructure.Services;
using Ictx.WebApp.Application.AppUnitOfWork;
using Ictx.WebApp.Infrastructure.UnitOfWork;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Ictx.WebApp.Infrastructure.AppStartUp.Configurations
{
    public static class ServicesConfiguration
    {
        public static IServiceCollection ConfigureServices(this IServiceCollection services)
        {
            // Services.
            services.TryAddSingleton<IDateTimeService, DateTimeService>();

            // Unit of work.
            services.TryAddScoped<IAppUnitOfWork, AppUnitOfWork>();

            return services;
        }
    }
}
