using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Ictx.WebApp.Infrastructure.Common;
using Ictx.WebApp.Templates.Mail;
using Ictx.WebApp.Application.Contracts.Services;
using Ictx.WebApp.Infrastructure.Services;
using Ictx.WebApp.Application.Contracts.UnitOfWork;
using Ictx.WebApp.Infrastructure.UnitOfWork;
using Ictx.WebApp.Infrastructure.Data.App;

namespace Ictx.WebApp.Infrastructure.DependencyInjection
{
    public static class InfrastructureExtension
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.ConfigureMail(configuration);
            services.ConfigureInfrastructureServices();
            services.ConfigureAppDbContext(configuration);

            return services;
        }

        private static IServiceCollection ConfigureMail(this IServiceCollection services, IConfiguration configuration)
        {
            // Mail settings.
            services.Configure<MailSettings>(configuration.GetSection(nameof(MailSettings)));
            services.AddSingleton<IMailSettings>(sp => sp.GetRequiredService<IOptions<MailSettings>>().Value);

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

        private static IServiceCollection ConfigureAppDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options => {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName));
            });

            return services;
        }
    }
}
