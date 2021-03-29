using System;
using System.IO;
using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Serialization;
using Swashbuckle.AspNetCore.SwaggerGen;
using Ictx.WebApp.Api.Common;
using Ictx.WebApp.Api.Common.Swagger;
using Ictx.WebApp.Api.Helper;

namespace Ictx.WebApp.Api.AppStartUp.Installers
{
    public class MvcInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            // Validation.
            services.Configure<ApiBehaviorOptions>(opt =>
            {
                opt.SuppressModelStateInvalidFilter = true;
            });

            // Cors.
            services.AddCors(options =>
            {
                options.AddPolicy(ApiHelper.AnyCors,
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
            }
            ).AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ContractResolver = new DefaultContractResolver();
            });

            // Api versioning.
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

            // Swagger.
            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
            services.AddSwaggerGen(
                options =>
                {
                    // add a custom operation filter which sets default values
                    options.OperationFilter<SwaggerDefaultValues>();

                    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

                    options.IncludeXmlComments(xmlPath);

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
    }
}
