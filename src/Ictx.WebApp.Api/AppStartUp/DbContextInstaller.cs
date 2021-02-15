using Ictx.WebApp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace Ictx.WebApp.Api.AppStartUp
{
    public class DbContextInstaller: IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            // DB context.
            services.AddDbContext<AppDbContext>(options => options
                .UseSqlServer(configuration.GetConnectionString("DefaultConnection"))
                .LogTo(Log.Information, Microsoft.Extensions.Logging.LogLevel.Information));
        }
    }
}
