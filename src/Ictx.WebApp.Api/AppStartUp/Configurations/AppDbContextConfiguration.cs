using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Ictx.WebApp.Infrastructure.Data;

namespace Ictx.WebApp.Api.AppStartUp.Configurations
{
    public static class AppDbContextConfiguration
    {
        /// <summary>Configura la Dependency Injection aggiungendo la configurazione letta dall'appsettings.json.</summary>
        public static IServiceCollection ConfigureAppDbContext(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment env)
        {
            // DB context.
            services.AddDbContext<AppDbContext>(options => {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly(typeof(Startup).Assembly.FullName));

                if (env.IsDevelopment())
                {
                    options.LogTo(Log.Debug);
                }            
            });

            return services;
        }
    }
}
