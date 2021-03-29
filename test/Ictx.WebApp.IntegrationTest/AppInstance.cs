using System.Linq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Ictx.WebApp.Api;
using Ictx.WebApp.Infrastructure.Data;

namespace Ictx.WebApp.IntegrationTest
{
    public class AppInstance : WebApplicationFactory<Startup>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                ReplaceDbContext(services);

                builder.UseEnvironment("Development");
            });
        }

        private void ReplaceDbContext(IServiceCollection services)
        {
            var appDbContext = services.SingleOrDefault(d => d.ServiceType == typeof(AppDbContext));
            var dbContextOptions = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<AppDbContext>));

            // Rimuovo la configurazione del database.
            services.Remove(appDbContext);
            services.Remove(dbContextOptions);

            // Aggiungo la configurazione del database in memory.
            services.AddDbContext<AppDbContext>(options => { options.UseInMemoryDatabase("TestDb"); });
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}
