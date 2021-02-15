using Ictx.WebApp.Api.AppStartUp;
using Ictx.WebApp.Api.Common;
using Ictx.WebApp.Api.SeedDatabase;
using Ictx.WebApp.Infrastructure.Data;
using Ictx.WebApp.Infrastructure.Services;
using Ictx.WebApp.Infrastructure.UnitOfWork;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Serialization;
using Serilog;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.IO;
using System.Reflection;

namespace Ictx.WebApp.Server
{
    public class Startup
    {
        private readonly IConfiguration _configuration;
        private readonly string _corsAny;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
            _corsAny = "any";
        }


        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            // DB context.
            services.AddDbContext<AppDbContext>(options => options
                .UseSqlServer(_configuration.GetConnectionString("DefaultConnection"))
                .LogTo(Log.Information, Microsoft.Extensions.Logging.LogLevel.Information));

            // Automapper.
            services.AddAutoMapperConfig();

            // Validation.
            services.Configure<ApiBehaviorOptions>(opt =>
            {
                opt.SuppressModelStateInvalidFilter = true;
            });

            AddDependencyInjection(services);

            // Cors.
            services.AddCors(options =>
            {
                options.AddPolicy(_corsAny,
                                  builder =>
                                  {
                                      builder.AllowAnyOrigin()
                                                          .AllowAnyHeader()
                                                          .AllowAnyMethod();
                                  });
            });

            // Filter.
            services.AddControllers(options =>
            {
                options.Filters.Add(typeof(ValidateModelControllerFilterr));
            }).AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ContractResolver = new DefaultContractResolver();
            });

            services.AddApiVersioning(
                options =>
                {
                    // reporting api versions will return the headers "api-supported-versions" and "api-deprecated-versions"
                    options.ReportApiVersions = true;
                });
            
            services.AddVersionedApiExplorer(
                options =>
                {
                    // add the versioned api explorer, which also adds IApiVersionDescriptionProvider service
                    // note: the specified format code will format the version as "'v'major[.minor][-status]"
                    options.GroupNameFormat = "'v'VVV";
                    // note: this option is only necessary when versioning by url segment. the SubstitutionFormat
                    // can also be used to control the format of the API version in route templates
                    options.SubstituteApiVersionInUrl = true;
                });

            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
            services.AddSwaggerGen(
                options =>
                {
                    // add a custom operation filter which sets default values
                    options.OperationFilter<SwaggerDefaultValues>();

                    //options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                    //{
                    //    In = ParameterLocation.Header,
                    //    Description = "Please insert JWT with Bearer into field",
                    //    Name = "Authorization",
                    //    Type = SecuritySchemeType.ApiKey
                    //});
                    //options.AddSecurityRequirement(new OpenApiSecurityRequirement {
                    //    {
                    //        new OpenApiSecurityScheme{
                    //           Reference = new OpenApiReference
                    //           {
                    //             Type = ReferenceType.SecurityScheme,
                    //             Id = "Bearer"
                    //           }
                    //        },
                    //        new string[] { }
                    //    }
                    //});
                });
        }

        private static void AddDependencyInjection(IServiceCollection services)
        {
            // Services.
            services.TryAddScoped<DipendenteService>();

            // Unit of work.
            services.TryAddScoped<AppUnitOfWork>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IApiVersionDescriptionProvider provider)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            app.UseRouting();

            if (!env.IsProduction())
            {
                // Swagger.
                app.UseSwagger();
                app.UseSwaggerUI(options =>
                {
                    // build a swagger endpoint for each discovered API version
                    foreach (var description in provider.ApiVersionDescriptions)
                    {
                        options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());
                    }
                }
                );
            }

            // Serilog.
            Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(_configuration)
            .CreateLogger();

            // Seed database.
            var seedDatabase = new SeedDatabase(app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope().ServiceProvider.GetRequiredService<AppDbContext>());
            seedDatabase.Initialize();


            // Cors.
            app.UseCors(_corsAny);

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
