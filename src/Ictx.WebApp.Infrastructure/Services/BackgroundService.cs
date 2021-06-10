using System.Threading.Tasks;
using Ictx.WebApp.Application.Services;
using Microsoft.Extensions.Logging;

namespace Ictx.WebApp.Infrastructure.Services
{
    public class BackgroundService : IBackgroundService
    {
        private readonly ILogger<BackgroundService> _logger;

        public BackgroundService(ILogger<BackgroundService> logger)
        {
            _logger = logger;
        }

        public async Task DoWork()
        {
            this._logger.LogInformation("Working...");

            await Task.CompletedTask;
        }
    }
}
