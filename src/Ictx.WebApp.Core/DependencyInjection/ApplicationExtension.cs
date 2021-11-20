using Ictx.WebApp.Core.BO;
using Ictx.WebApp.Templates.Mail;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Ictx.WebApp.Core.DependencyInjection
{
    public static class ApplicationExtension
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            // Services.
            services.TryAddScoped<DipendenteBO>();

            services.AddScoped<IRazorViewService, RazorViewService>();

            return services;
        }
    }
}
