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
using System.Transactions;

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
                orderBy: x => x.OrderBy(o => o.Inserted));

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
                await SendMail(cancellationToken);
            }

            //Task.WaitAll(taskList.ToArray());
        }

        private async Task SendMail(CancellationToken cancellationToken)
        {
            this._backgroundServiceUnitOfWork.BeginTransaction();

            var operazione = await this._backgroundServiceUnitOfWork.OperationRepository.GetNextOperation(BackgroundOperationType.Mail);

            if (cancellationToken.IsCancellationRequested || operazione is null)
            {
                return;
            }

            operazione.Started = true;

            this._backgroundServiceUnitOfWork.OperationRepository.Update(operazione);
            await this._backgroundServiceUnitOfWork.SaveAsync();

            this._backgroundServiceUnitOfWork.CommitTransaction();

            try
            {
                var mail = JsonConvert.DeserializeObject<MailModel>(operazione.Data);
                this._logger.LogInformation($"Sending mail to: {mail.Mail}");

                await this._mailService.SendEmail(mail, cancellationToken);

                operazione.Completed = true;

                this._backgroundServiceUnitOfWork.OperationRepository.Update(operazione);
                await this._backgroundServiceUnitOfWork.SaveAsync();
            }
            catch (Exception e)
            {
                this._logger.LogError(e, "BackgroundServiceBO.SendMail Errore durante l'invio della mail.");
            }  

            // Prossima operazione.
            await SendMail(cancellationToken);
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
