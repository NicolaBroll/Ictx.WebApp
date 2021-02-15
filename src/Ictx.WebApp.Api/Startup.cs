using Ictx.WebApp.Api.AppStartUp;
using Ictx.WebApp.Api.Common;
using Ictx.WebApp.Api.SeedDatabase;
using Ictx.WebApp.Infrastructure.Data;
using Ictx.WebApp.Infrastructure.Services.Implementation;
using Ictx.WebApp.Infrastructure.Services.Interface;
using Ictx.WebApp.Infrastructure.UnitOfWork;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Serialization;
using Serilog;

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

            // Swagger.
            services.AddSwaggerGen(c => {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Application api", Version = "V1" });
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

        }

        private static void AddDependencyInjection(IServiceCollection services)
        {
            // Services.
            services.TryAddScoped<IDipendenteService, DipendenteService>();

            // Unit of work.
            services.TryAddScoped<AppUnitOfWork>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            if (!env.IsProduction())
            {
                // Swagger.
                app.UseSwagger();
                app.UseSwaggerUI(c => {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "API V1");
                });
            }

            // Serilog.
            Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(_configuration)
            .CreateLogger();

            // Seed database.
            var seedDatabase = new SeedDatabase(app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope().ServiceProvider.GetRequiredService<AppDbContext>());
            seedDatabase.Initialize();

            app.UseRouting();

            // Cors.
            app.UseCors(_corsAny);

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
