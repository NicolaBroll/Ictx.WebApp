using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Ictx.WebApp.Api.Data;
using Ictx.WebApp.Infrastructure.UnitOfWork;
using Serilog;

namespace Ictx.WebApp.Api
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
                    var appUnitOfWork = services.GetRequiredService<IAppUnitOfWork>();

                    // Seed database.
                    var seedDatabase = new SeedDatabase(appUnitOfWork);                
                    await seedDatabase.Initialize();
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred seeding the DB.");
                }
            }

            await host.RunAsync();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                    .UseSerilog((context, services, configuration) => configuration.ReadFrom.Configuration(context.Configuration))
                    .ConfigureWebHostDefaults(webBuilder =>
                    {
                        webBuilder.UseStartup<Startup>();
                    });
        }
    }
}
