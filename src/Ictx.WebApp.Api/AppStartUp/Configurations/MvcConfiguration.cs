﻿using System;
using System.IO;
using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;
using Ictx.WebApp.Api.Common.Swagger;
using Ictx.WebApp.Api.Helper;
using Ictx.WebApp.Api.AppStartUp.Middlewares;

namespace Ictx.WebApp.Api.AppStartUp.Configurations;

public static class MvcConfiguration
{
    public static IServiceCollection ConfigureMvc(this IServiceCollection services)
    {
        services.AddHttpContextAccessor();

        services.ConfigureSessionData();

        // Aggiungo la gestione globale dell'eccezione in caso di internal server error.
        services.AddTransient<ExceptionHandlingMiddleware>();

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

        services
            .AddControllers();

        // Api versioning.
        services.AddApiVersioning(
            options =>
            {
                // Specify the default API Version as 1.0
                options.DefaultApiVersion = new ApiVersion(1, 0);

                // If the client hasn't specified the API version in the request, use the default API version number
                options.AssumeDefaultVersionWhenUnspecified = true;

                // Advertise the API versions supported for the particular endpoint
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

        return services;
    }
}
