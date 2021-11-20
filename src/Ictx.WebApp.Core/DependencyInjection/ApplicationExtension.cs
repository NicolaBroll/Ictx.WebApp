using FluentValidation;
using Ictx.WebApp.Core.BO;
using Ictx.WebApp.Core.Data;
using Ictx.WebApp.Core.Entities;
using Ictx.WebApp.Core.Validators;
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

            // Validators.
            services.AddScoped<IValidator<Dipendente>, DipendenteValidator>();

            // Fake data generator.
            services.AddTransient<FakeDataGenerator>();

            return services;
        }
    }
}
