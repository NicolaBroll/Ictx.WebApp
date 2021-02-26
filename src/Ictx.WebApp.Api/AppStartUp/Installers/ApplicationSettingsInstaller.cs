using Ictx.WebApp.Api.Common;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Ictx.WebApp.Api.AppStartUp.Installers
{
    public class ApplicationSettingsInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<ApplicationSettings>(
                configuration.GetSection(nameof(ApplicationSettings)));

            services.AddSingleton<IApplicationSettings>(sp =>
                sp.GetRequiredService<IOptions<ApplicationSettings>>().Value);
        }
    }
}