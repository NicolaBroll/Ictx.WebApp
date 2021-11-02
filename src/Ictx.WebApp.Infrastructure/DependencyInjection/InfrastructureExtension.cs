using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Ictx.WebApp.Infrastructure.Common;
using Ictx.WebApp.Templates.Mail;
using Ictx.WebApp.Application.Contracts.Services;
using Ictx.WebApp.Infrastructure.Services;
using Ictx.WebApp.Application.Contracts.UnitOfWork;
using Ictx.WebApp.Infrastructure.UnitOfWork;
using Ictx.WebApp.Infrastructure.Data.App;
using System;

namespace Ictx.WebApp.Infrastructure.DependencyInjection
{
    public static class InfrastructureExtension
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, InfrastructureOptions infrastructureOptions)
        {
            if (infrastructureOptions is null)
            {
                throw new ArgumentNullException(nameof(infrastructureOptions));
            }

            services.ConfigureMail(infrastructureOptions.MailSettings);
            services.ConfigureInfrastructureServices();
            services.ConfigureAppDbContext(infrastructureOptions.ConnectionString);

            return services;
        }

        private static IServiceCollection ConfigureMail(this IServiceCollection services, MailSettings mailSettings)
        {
            // Mail settings.
            services.AddSingleton<IMailSettings>(sp => mailSettings);

            // Razor pages per il render della mail.
            services.AddRazorPages();

            // Mail services.
            services.AddScoped<IRazorViewService, RazorViewService>();
            services.AddScoped<IMailService, MailService>();

            return services;
        }

        private static IServiceCollection ConfigureInfrastructureServices(this IServiceCollection services)
        {
            // Services.
            services.TryAddSingleton<IDateTimeService, DateTimeService>();

            // Unit of work.
            services.TryAddScoped<IAppUnitOfWork, AppUnitOfWork>();

            return services;
        }

        private static IServiceCollection ConfigureAppDbContext(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<AppDbContext>(options => {
                options.UseSqlServer(connectionString, b => b.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName));
            });

            return services;
        }
    }
}
