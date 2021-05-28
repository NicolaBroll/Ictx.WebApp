using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Ictx.WebApp.Api.AppStartUp.Configurations
{
    public static class AuthenticationConfiguration
    {
        public static IServiceCollection ConfigureAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication("Bearer")
                .AddJwtBearer("Bearer", options =>
                {
                    options.Authority = "https://localhost:6001";
                    options.Audience = "Api_1";

                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateAudience = true,
                        ValidAudience = "Api_1",
                        ValidateIssuer = true,
                        ValidIssuer = "https://localhost:6001"
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
