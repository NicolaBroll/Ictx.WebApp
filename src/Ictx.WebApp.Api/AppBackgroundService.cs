using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;

namespace Ictx.WebApp.Api
{
    public class AppBackgroundService : BackgroundService
    {
        private readonly ILogger<AppBackgroundService>  _logger;
        private readonly IServiceProvider               _serviceProvider;

        public AppBackgroundService(ILogger<AppBackgroundService> logger, IServiceProvider serviceProvider)
        {
            this._logger            = logger;
            this._serviceProvider   = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var delay = 5;

            this._logger.LogInformation("AppBackgroundService partito.");

            while (!stoppingToken.IsCancellationRequested)
            {
                using (var scope = this._serviceProvider.CreateScope())
                {
                    this._logger.LogInformation("AppBackgroundService Run.");
                }

                await Task.Delay(TimeSpan.FromSeconds(delay), stoppingToken);
            }
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            this._logger.LogInformation("AppBackgroundService fermato.");
            return base.StopAsync(cancellationToken);
        }
    }
}
