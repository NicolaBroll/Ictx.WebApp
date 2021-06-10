using Ictx.WebApp.Application.Services;
using Ictx.WebApp.BackGroundService.Common;
using Ictx.WebApp.Infrastructure.AppStartUp.Configurations;
using Ictx.WebApp.Infrastructure.Data.BackgroundService;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Serilog;
using System;
using System.Threading.Tasks;

namespace Ictx.WebApp.BackGroundService
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                try
                {
                    // Seed database.
                    SeedDatabase(services);
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred seeding the DB.");
                }
            }

            await host.RunAsync();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)            
                .ConfigureServices((hostContext, services) =>
                {
                    // Services.
                    services.AddScoped<IBackgroundService, Infrastructure.Services.BackgroundService>();

                    // App config.
                    services.Configure<ApplicationSettings>(hostContext.Configuration.GetSection(nameof(ApplicationSettings)));
                    services.AddSingleton<IApplicationSettings>(sp => sp.GetRequiredService<IOptions<ApplicationSettings>>().Value);

                    // BackgroundServiceDbContext.
                    services.ConfigureBackgroundServiceDbContext(options => {
                        options.UseSqlServer(hostContext.Configuration.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly(typeof(Program).Assembly.FullName));

                        if (hostContext.HostingEnvironment.IsDevelopment())
                        {
                            options.LogTo(Log.Debug);
                        }
                    });

                    services.AddHostedService<Worker>();
                })
            .UseSerilog((context, services, configuration) => configuration.ReadFrom.Configuration(context.Configuration));

        private static void SeedDatabase(IServiceProvider services)
        {
            var dtc = services.GetRequiredService<BackgroundServiceDbContext>();

            dtc.Database.EnsureCreated();
        }
    }
}
