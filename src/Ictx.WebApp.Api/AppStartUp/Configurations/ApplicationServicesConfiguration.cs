using Ictx.WebApp.Templates.Mail;
using Ictx.WebApp.Application.BO;
using Ictx.WebApp.Core.Interfaces;
using Ictx.WebApp.Infrastructure.Application;
using Ictx.WebApp.Infrastructure.Services;
using Ictx.WebApp.Infrastructure.UnitOfWork;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Ictx.WebApp.Api.AppStartUp.Configurations
{
    public static class ApplicationServicesConfiguration
    {
        /// <summary>Configura la Dependency Injection aggiungendo tutti i servizi che utilizza l'app.</summary>
        public static IServiceCollection ConfigureApplicationServices(this IServiceCollection services)
        {
            // Services.
            services.TryAddSingleton<IDateTimeService, DateTimeService>();
            services.TryAddScoped<DipendenteBO>();
            services.AddScoped<IRazorViewToStringRenderer, RazorViewToStringRenderer>();
            services.AddScoped<IMailService, MailService>();

            // Unit of work.
            services.TryAddScoped<IAppUnitOfWork, AppUnitOfWork>();

            return services;
        }
    }
}
