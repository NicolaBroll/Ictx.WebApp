using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Ictx.WebApp.Infrastructure.AppStartUp.Configurations;

namespace Ictx.WebApp.Api.AppStartUp.Configurations
{
    public static class DbContextConfiguration
    {
        public static IServiceCollection ConfigureDbContext(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment env)
        {
            // App db context.
            services.ConfigureAppDbContext(options => {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly(typeof(Startup).Assembly.FullName));

                if (env.IsDevelopment())
                {
                    options.LogTo(Log.Debug);
                }
            });

            // BackgroundService db context.
            services.ConfigureBackgroundServiceDbContext(options => {
                options.UseSqlServer(configuration.GetConnectionString("BackgroundServiceConnection"), b => b.MigrationsAssembly(typeof(Startup).Assembly.FullName));

                //if (env.IsDevelopment())
                //{
                //    options.LogTo(Log.Debug);
                //}
            });

            return services;
        }
    }
}
