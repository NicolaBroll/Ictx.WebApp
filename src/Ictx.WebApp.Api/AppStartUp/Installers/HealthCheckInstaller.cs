using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Ictx.WebApp.Api.AppStartUp.Installers
{
    public class HealthCheckInstaller: IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        { 
            services.AddHealthChecks();
        }
    }
}
