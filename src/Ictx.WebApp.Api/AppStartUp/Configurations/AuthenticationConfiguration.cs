using Ictx.WebApp.Api.Common;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Ictx.WebApp.Api.AppStartUp.Configurations
{
    public static class AuthenticationConfiguration
    {
        public static IServiceCollection ConfigureAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            var settings = new AuthSettings();
            configuration.GetSection(nameof(AuthSettings)).Bind(settings);

            services.AddAuthentication("Bearer")
                .AddJwtBearer("Bearer", options =>
                {
                    // Base-address identityserver.
                    options.Authority = settings.Authority;

                    // Audiance.
                    options.Audience = settings.Audience;

                    // IdentityServer emits a typ header by default, recommended extra check
                    options.TokenValidationParameters.ValidTypes = new[] { "at+jwt" };

                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        //ValidateIssuerSigningKey = true,
                        ValidIssuer = settings.Authority,
                        ValidAudience = settings.Audience,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(settings.Key))
                    };
                });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("Api1_r", policy => policy.RequireClaim("scope", "api1_r"));
                options.AddPolicy("Api1_w", policy => policy.RequireClaim("scope", "api1_w"));
            });

            return services;
        }
    }
}
