using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Ictx.WebApp.Infrastructure.Data.App;
using Ictx.WebApp.Infrastructure.Data.BackgroundService;

namespace Ictx.WebApp.Infrastructure.AppStartUp.Configurations
{
    public static class AppDbContextConfiguration
    {
        public static IServiceCollection ConfigureAppDbContext(this IServiceCollection services, Action<DbContextOptionsBuilder> optionsAction = null)
        {
            services.AddDbContext<AppDbContext>(optionsAction);

            return services;
        }

        public static IServiceCollection ConfigureBackgroundServiceDbContext(this IServiceCollection services, Action<DbContextOptionsBuilder> optionsAction = null)
        {
            services.AddDbContext<BackgroundServiceDbContext>(optionsAction);

            return services;
        }
    }
}
