using Microsoft.Extensions.Logging;
using Ictx.WebApp.Core.Entities;
using Ictx.WebApp.Application.Models;
using System.Threading.Tasks;
using System.Threading;
using System;
using Newtonsoft.Json;
using Ictx.WebApp.Application.AppUnitOfWork;

namespace Ictx.WebApp.Application.BO
{
    public class BackgroundServiceBO : BaseBO<Operation, int, PaginationModel>
    {
        private readonly ILogger<BackgroundServiceBO> _logger;
        private readonly IBackgroundServiceUnitOfWork _backgroundServiceUnitOfWork;

        public BackgroundServiceBO(ILogger<BackgroundServiceBO> logger, IBackgroundServiceUnitOfWork backgroundServiceUnitOfWork) : base(logger, null)
        {
            this._logger = logger;
            this._backgroundServiceUnitOfWork = backgroundServiceUnitOfWork;
        }

        protected override async Task<OperationResult<Operation>> InsertViewAsync(Operation value, CancellationToken cancellationToken)
        {
            await this._backgroundServiceUnitOfWork.OperationRepository.InsertAsync(value, cancellationToken);
            await this._backgroundServiceUnitOfWork.SaveAsync();

            return new OperationResult<Operation>(value);
        }

        public async Task DoWork(CancellationToken cancellationToken)
        {
            this._logger.LogInformation("Working...");

            await Task.CompletedTask;
        }

        public static Operation CreateOperation<T>(T data, Guid utenteIdRequest)
        {
            return new Operation
            {
                UserId = utenteIdRequest,
                Data = JsonConvert.SerializeObject(data)
            };
        }
    }
}
