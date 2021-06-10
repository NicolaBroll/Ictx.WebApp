using Ictx.WebApp.Api.Common;
using Ictx.WebApp.Application.BO;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Ictx.WebApp.Api
{
    public class Worker : BackgroundService
    {
        private readonly IServiceProvider _services;
        private readonly ILogger<Worker> _logger;
        private readonly IApplicationSettings _applicationSettings;

        public Worker(IServiceProvider services, ILogger<Worker> logger, IApplicationSettings applicationSettings)
        {
            this._services = services;
            this._logger = logger;
            this._applicationSettings = applicationSettings;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            this._logger.LogInformation("Work start with: {delay} delay", this._applicationSettings.ExecutionDelay);

            while (!stoppingToken.IsCancellationRequested)
            {
                using (var scope = _services.CreateScope())
                {
                    var gestoreElaborazioniService = scope.ServiceProvider.GetRequiredService<BackgroundServiceBO>();
                    await gestoreElaborazioniService.DoWork();
                }

                await Task.Delay(this._applicationSettings.ExecutionDelay * 1000, stoppingToken);
            }
        }

        public override async Task StopAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Stopping...");
            await base.StopAsync(stoppingToken);
        }
    }
}
