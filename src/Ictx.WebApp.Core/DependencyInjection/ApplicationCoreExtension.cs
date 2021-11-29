using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using FluentValidation;
using Ictx.WebApp.Core.Data.App;
using Ictx.WebApp.Core.Services;
using Ictx.WebApp.Core.Domain.Dipendente;
using Ictx.WebApp.Core.Domain.Utente;

namespace Ictx.WebApp.Core.DependencyInjection
{
    public static class ApplicationCoreExtension
    {
        public static IServiceCollection AddApplicationCore(this IServiceCollection services, ApplicationCoreOptions infrastructureOptions)
        {
            if (infrastructureOptions is null)
            {
                throw new ArgumentNullException(nameof(infrastructureOptions));
            }

            services.ConfigureMail(infrastructureOptions.MailSettings);
            services.ConfigureInfrastructureServices();
            services.ConfigureAppDbContext(infrastructureOptions.ConnectionString);

            // Services.
            services.AddScoped<DipendenteBO>();
            services.AddScoped<UtenteBO>();

            // Validators.
            services.AddScoped<IValidator<Dipendente>, DipendenteValidator>();

            // Fake data generator.
            services.AddTransient<FakeDataGenerator>();

            return services;
        }

        private static IServiceCollection ConfigureMail(this IServiceCollection services, MailSettings mailSettings)
        {
            // Mail settings.
            services.AddSingleton<IMailSettings>(sp => mailSettings);

            // Razor pages per il render della mail.
            services.AddRazorPages();

            // Mail services.
            services.AddScoped<IMailService, MailService>();

            return services;
        }

        private static IServiceCollection ConfigureInfrastructureServices(this IServiceCollection services)
        {
            // Services.
            services.AddSingleton<IDateTimeService, DateTimeService>();
            services.AddScoped<IRazorViewService, RazorViewService>();

            return services;
        }

        private static IServiceCollection ConfigureAppDbContext(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<AppDbContext>(options => {
                options.UseInMemoryDatabase(nameof(AppDbContext));
                //options.UseSqlServer(connectionString, b => b.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName));
            });

            return services;
        }
    }
}
