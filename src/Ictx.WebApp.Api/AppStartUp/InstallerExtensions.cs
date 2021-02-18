using System;
using System.Linq;
using Ictx.WebApp.Api.AppStartUp.Installers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Ictx.WebApp.Api.AppStartUp
{
    public static class InstallerExtensions
    {
        public static void InstallServiceAssembly(this IServiceCollection services, IConfiguration configuration) 
        {
            var installs = typeof(Startup).Assembly.ExportedTypes
            .Where(x => typeof(IInstaller).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract)
            .Select(Activator.CreateInstance).Cast<IInstaller>().ToList();

            installs.ForEach(x => x.InstallServices(services, configuration));
        }
    }
}
