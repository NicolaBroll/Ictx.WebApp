using Microsoft.Extensions.Logging;
using Ictx.WebApp.Core.Entities;
using Ictx.WebApp.Application.Models;
using System.Threading.Tasks;

namespace Ictx.WebApp.Application.BO
{
    public class BackgroundServiceBO : BaseBO<Operation, int, PaginationModel>
    {
        private readonly ILogger<BackgroundServiceBO> _logger;

        public BackgroundServiceBO(ILogger<BackgroundServiceBO> logger): base(logger, null)
        {
            this._logger = logger;
        }

        public async Task DoWork()
        {
            this._logger.LogInformation("Working...");

            await Task.CompletedTask;
        }
    }
}
