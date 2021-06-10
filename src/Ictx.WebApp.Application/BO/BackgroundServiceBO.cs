using Microsoft.Extensions.Logging;
using Ictx.WebApp.Core.Entities;
using Ictx.WebApp.Application.Models;
using System.Threading.Tasks;
using System.Threading;
using System;
using Newtonsoft.Json;
using Ictx.WebApp.Application.UnitOfWork;
using System.Linq;
using System.Collections.Generic;
using Ictx.WebApp.Application.Services;
using Ictx.WebApp.Core.Models;

namespace Ictx.WebApp.Application.BO
{
    public class BackgroundServiceBO : BaseBO<Operation, int, PaginationModel>
    {
        private readonly ILogger<BackgroundServiceBO>   _logger;
        private readonly IBackgroundServiceUnitOfWork   _backgroundServiceUnitOfWork;
        private readonly IMailService                   _mailService;

        public BackgroundServiceBO(ILogger<BackgroundServiceBO> logger,
            IBackgroundServiceUnitOfWork backgroundServiceUnitOfWork,
            IMailService mailService) : base(logger, null)
        {
            this._logger = logger;
            this._backgroundServiceUnitOfWork = backgroundServiceUnitOfWork;
            this._mailService = mailService;
        }

        protected override async Task<OperationResult<Operation>> InsertViewAsync(Operation value, CancellationToken cancellationToken)
        {
            await this._backgroundServiceUnitOfWork.OperationRepository.InsertAsync(value, cancellationToken);
            await this._backgroundServiceUnitOfWork.SaveAsync();

            return new OperationResult<Operation>(value);
        }

        protected override async Task<IEnumerable<Operation>> ReadManyViewsAsync(PaginationModel filter, CancellationToken cancellationToken)
        {
            var operations = await this._backgroundServiceUnitOfWork.OperationRepository.ReadManyAsync(
                filter: x => !x.Started,
                orderBy: x => x.OrderBy(x => x.Inserted));

            return operations;
        }

        public async Task DoWork(CancellationToken cancellationToken)
        {
            var operations = await ReadManyViewsAsync(null, cancellationToken);
            
            this._logger.LogInformation($"Working... Found {operations.Count()} operations");

            // Se non ci saono operazioni, esco.
            if (!operations.Any()) 
            {
                return;
            }

            var taskList = new List<Task>();
            var mailOperations = operations.Where(x => x.Tipo == BackgroundOperationType.Mail).ToList();

            if (mailOperations.Any()) 
            {
                // Invio le mail.
                taskList.Add(SendMail(mailOperations, cancellationToken));
            }

            Task.WaitAll(taskList.ToArray());
        }

        private async Task SendMail(List<Operation> mailOperations, CancellationToken cancellationToken)
        {
            foreach (var mailOperation in mailOperations)
            {
                if (cancellationToken.IsCancellationRequested) 
                {
                    break;
                }

                try
                {
                    var mail = JsonConvert.DeserializeObject<MailModel>(mailOperation.Data);
                    this._logger.LogInformation($"Sending mail to: {mail.Mail}");

                    await this._mailService.SendEmail(mail, cancellationToken);
                }
                catch (Exception e)
                {
                    this._logger.LogError(e, "BackgroundServiceBO.SendMail Errore durante l'invio della mail.");
                }
            }
        }

        public static Operation CreateOperation<T>(T data, BackgroundOperationType tipo, Guid utenteIdRequest)
        {
            return new Operation
            {
                UserId = utenteIdRequest,
                Data = JsonConvert.SerializeObject(data),
                Tipo = tipo
            };
        }
    }
}
