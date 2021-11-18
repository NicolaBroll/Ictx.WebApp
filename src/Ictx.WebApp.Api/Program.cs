using System.Threading.Tasks;
using Ictx.WebApp.Application.Data;
using Ictx.WebApp.Infrastructure.Data.App;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

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