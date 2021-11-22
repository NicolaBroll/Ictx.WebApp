using System.Linq;
using System.Text.Json;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Ictx.WebApp.Api.Helper;
using Ictx.WebApp.Api.Common.HealthCheck;
using Ictx.WebApp.Infrastructure.DependencyInjection;
using Ictx.WebApp.Core.DependencyInjection;
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
        services.AddHealthChecks();//.AddDbContextCheck<AppDbContext>();

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

        // Background service.
        services.AddHostedService<AppBackgroundService>();
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

        //// Exception middleware.
        //app.UseMiddleware<ExceptionHandlingMiddleware>();

        if (!env.IsDevelopment())
        {
            // Do not add exception handler for dev environment. In dev,
            // we get the developer exception page with detailed error info.
            app.UseExceptionHandler(errorApp =>
            {
                // Logs unhandled exceptions. For more information about all the
                // different possibilities for how to handle errors see
                // https://docs.microsoft.com/en-us/aspnet/core/fundamentals/error-handling?view=aspnetcore-5.0
                errorApp.Run(async context =>
                {
                    // Return machine-readable problem details. See RFC 7807 for details.
                    // https://datatracker.ietf.org/doc/html/rfc7807#page-6
                    var pd = new ProblemDetails
                    {
                        Type = "https://demo.api.com/errors/internal-server-error",
                        Title = "Server error",
                        Status = StatusCodes.Status500InternalServerError,
                        Detail = "Server error",
                    };
                    pd.Extensions.Add("RequestId", context.TraceIdentifier);
                    await context.Response.WriteAsJsonAsync(pd, pd.GetType(), null, contentType: "application/problem+json");
                });
            });
        }


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
