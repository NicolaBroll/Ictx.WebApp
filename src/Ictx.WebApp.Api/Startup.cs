using System.Linq;
using System.Text.Json;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Ictx.WebApp.Api.Helper;
using Ictx.WebApp.Api.Common.HealthCheck;
using Ictx.WebApp.Infrastructure.Data.App;
using Ictx.WebApp.Infrastructure.DependencyInjection;
using Ictx.WebApp.Application.DependencyInjection;
using Ictx.WebApp.Infrastructure.Common;
using Ictx.WebApp.Api.AppStartUp.Configurations;

namespace Ictx.WebApp.Api;

public class Startup
{
    private readonly IConfiguration _configuration;
    private readonly IWebHostEnvironment _webHostEnvironment;

    public Startup(IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
    {
        this._configuration = configuration;
        this._webHostEnvironment = webHostEnvironment;
    }

    /// <summary>
    /// Configurazione dependency injection.
    /// </summary>
    /// <param name="services"></param>
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddHttpContextAccessor();

        // Application settings.
        services.ConfigureApplicationSettings(this._configuration);

        // Automapper.
        services.ConfigureAutomapper();

        // Health check.
        services.AddHealthChecks().AddDbContextCheck<AppDbContext>();

        // MVC.
        services.ConfigureMvc();

        // Authentication.
        services.ConfigureAuthentication(this._configuration);

        // Configuro i servizi consumati dall'app.
        services.AddApplication();

        // Infrastructure.
        var mailConfig = new MailSettings();
        this._configuration.Bind(nameof(MailSettings), mailConfig);

        services.AddInfrastructure(
            InfrastructureOptionsBuilder
                .Configure()
                .ApplicationDatacontextConfigurationStage(this._configuration.GetConnectionString("DefaultConnection"))
                .MailConfigurationStage(mailConfig)
        );
    }

    /// <summary>
    /// Moddleware.
    /// </summary>
    /// <param name="app"></param>
    /// <param name="env"></param>
    /// <param name="provider"></param>
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IApiVersionDescriptionProvider provider)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        if (!env.IsProduction())
        {
            // Swagger.
            app.UseSwagger();
            app.UseSwaggerUI(options => provider.ApiVersionDescriptions
                .ToList()
                .ForEach(description => options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant())));
        }

        app.UseRouting();

        // Cors.
        app.UseCors(ApiHelper.AnyCors);

        // Auth.
        app.UseAuthentication();
        app.UseAuthorization();

        // Health checks.
        app.UseHealthChecks("/health", new HealthCheckOptions()
        {
            ResponseWriter = async (context, report) => 
            {
                context.Response.ContentType = "application/json";

                var response = new HealthCheckResponse()
                {
                    Status = report.Status.ToString(),
                    Checks = report.Entries.Select(x => new HealthCheck 
                    {
                        Component = x.Key,
                        Status = x.Value.Status.ToString(),
                        Description = x.Value.Description
                    }),
                    Duration = report.TotalDuration
                };

                await context.Response.WriteAsync(JsonSerializer.Serialize(response));
            }
        });

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}
