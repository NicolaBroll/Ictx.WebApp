﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Ictx.WebApp.Application.Common;

namespace Ictx.WebApp.Api.AppStartUp.Configurations
{
    public static class ApplicationSettingsConfiguration
    {
        /// <summary>Configura la Dependency Injection aggiungendo la configurazione letta dall'appsettings.json.</summary>
        public static IServiceCollection ConfigureApplicationSettings(this IServiceCollection services, IConfiguration _configuration)
        {
            // Mail.
            services.Configure<MailSettings>(_configuration.GetSection(nameof(MailSettings)));
            services.AddSingleton<IMailSettings>(sp => sp.GetRequiredService<IOptions<MailSettings>>().Value);

            return services;
        }
    }
}
