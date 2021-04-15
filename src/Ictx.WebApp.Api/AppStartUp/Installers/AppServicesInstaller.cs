using Ictx.WebApp.Infrastructure.Services;
using Ictx.WebApp.Infrastructure.Services.Interfaces;
using Ictx.WebApp.Infrastructure.UnitOfWork;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Ictx.WebApp.Api.AppStartUp.Installers
{
    public class AppServicesInstaller: IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            // Services.
            services.TryAddScoped<IDittaService, DittaService>();
            services.TryAddScoped<IDipendenteService, DipendenteService>();
            services.TryAddSingleton<IDateTimeService, DateTimeService>();

            // Unit of work.
            services.TryAddScoped<AppUnitOfWork>();
        }
    }
}
