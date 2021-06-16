using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Ictx.WebApp.Infrastructure.Common;
using Ictx.WebApp.Templates.Mail;
using Ictx.WebApp.Application.Services;
using Ictx.WebApp.Infrastructure.Services;

namespace Ictx.WebApp.Infrastructure.AppStartUp.Configurations
{
    public static class MailConfiguration
    {
        public static IServiceCollection ConfigureMail(this IServiceCollection services, IConfiguration configuration)
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
    }
}
