using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Ictx.WebApp.Infrastructure.Data;
using System;

namespace Ictx.WebApp.Infrastructure.AppStartUp.Configurations
{
    public static class AppDbContextConfiguration
    {
        public static IServiceCollection ConfigureAppDbContext(this IServiceCollection services, Action<DbContextOptionsBuilder> optionsAction = null)
        {
            // DB context.
            services.AddDbContext<AppDbContext>(optionsAction);

            return services;
        }
    }
}
