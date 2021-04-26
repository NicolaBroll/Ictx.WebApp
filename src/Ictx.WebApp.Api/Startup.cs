using System.Linq;
using Newtonsoft.Json;
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
using Ictx.WebApp.Api.AppStartUp.Configurations;
using Ictx.WebApp.Infrastructure.Data;

namespace Ictx.WebApp.Api
{
    public class Startup
    {
        private readonly IConfiguration         _configuration;
        private readonly IWebHostEnvironment    _env;

        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            this._configuration = configuration;
            this._env           = env;
        }

        public void ConfigureServices(IServiceCollection services)
        {    
            // Application settings.
            services.ConfigureApplicationSettings(this._configuration);

            // AppDbContext.
            services.ConfigureAppDbContext(this._configuration, this._env);

            // Configuro i servizi consumati dall'app.
            services.ConfigureApplicationServices();

            // Automapper.
            services.ConfigureAutomapper();

            // Health check.
            services.AddHealthChecks().AddDbContextCheck<AppDbContext>();

            // MVC.
            services.ConfigureMvc();
        }

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

                    await context.Response.WriteAsync(JsonConvert.SerializeObject(response));
                }
            });

            app.UseRouting();

            // Cors.
            app.UseCors(ApiHelper.AnyCors);

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
