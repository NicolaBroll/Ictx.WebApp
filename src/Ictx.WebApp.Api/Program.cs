using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Ictx.WebApp.Core.Data;

namespace Ictx.WebApp.Api;

public class Program
{
    public static async Task Main(string[] args)
    {
        var host = CreateHostBuilder(args).Build();

        using (var scope = host.Services.CreateScope())
        {
            // Seed database.
            //var appDbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            //appDbContext.Database.Migrate();

            var fakeDataGenerator = scope.ServiceProvider.GetRequiredService<FakeDataGenerator>();
            await fakeDataGenerator.Genera();

            var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
            logger.LogInformation("WebApp partito.");
        }

        await host.RunAsync();
    }

    private static IHostBuilder CreateHostBuilder(string[] args)
    {
        return Host.CreateDefaultBuilder(args)
                   .UseSerilog((context, services, configuration) => configuration.ReadFrom.Configuration(context.Configuration))
                   .ConfigureWebHostDefaults(webBuilder =>
                   {
                       webBuilder.UseStartup<Startup>();
                   });
    }       
}