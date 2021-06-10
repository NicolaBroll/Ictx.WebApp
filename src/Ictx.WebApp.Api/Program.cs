using System;
using System.Threading.Tasks;
using Ictx.WebApp.Infrastructure.Data.App;
using Ictx.WebApp.Infrastructure.Data.BackgroundService;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
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
                var logger = services.GetRequiredService<ILogger<Program>>();
     
                // Seed database.
                SeedDatabase(services, logger); 
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

        private static void SeedDatabase(IServiceProvider services, ILogger<Program> logger)
        {
            try
            {
                var backgroundServiceDbContext = services.GetRequiredService<BackgroundServiceDbContext>();
                var appDbContext = services.GetRequiredService<AppDbContext>();

                backgroundServiceDbContext.Database.EnsureCreated();
                appDbContext.Database.EnsureCreated();            
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred seeding the DB.");
            }
        }
    }
}
