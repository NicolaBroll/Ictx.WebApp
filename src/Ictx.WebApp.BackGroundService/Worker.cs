using System;
using System.Threading;
using System.Threading.Tasks;
using Ictx.WebApp.Application.Services;
using Ictx.WebApp.BackGroundService.Common;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Ictx.WebApp.BackGroundService
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
                    var gestoreElaborazioniService = scope.ServiceProvider.GetRequiredService<IBackgroundService>();
                    await gestoreElaborazioniService.DoWork();
                }

                await Task.Delay(this._applicationSettings.ExecutionDelay * 1000, stoppingToken);
            }
        }
    }
}
