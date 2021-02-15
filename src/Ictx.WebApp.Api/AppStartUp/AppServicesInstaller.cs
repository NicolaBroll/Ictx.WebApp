using Ictx.WebApp.Infrastructure.Services;
using Ictx.WebApp.Infrastructure.UnitOfWork;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Ictx.WebApp.Api.AppStartUp
{
    public class AppServicesInstaller: IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            // Services.
            services.TryAddScoped<DipendenteService>();

            // Unit of work.
            services.TryAddScoped<AppUnitOfWork>();
        }
    }
}
