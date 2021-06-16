using Microsoft.Extensions.Logging;
using Ictx.WebApp.Core.Entities;
using Ictx.WebApp.Application.Models;
using System.Threading.Tasks;
using System.Threading;
using System;
using Newtonsoft.Json;
using Ictx.WebApp.Application.UnitOfWork;
using Ictx.WebApp.Application.Services;
using Ictx.WebApp.Core.Models;
using System.Collections.Generic;
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

        public async Task DoWork(CancellationToken cancellationToken)
        {
            var operazione = await this._backgroundServiceUnitOfWork.OperationRepository.GetAndStartNextOperazione(cancellationToken);

            if (operazione is null)
            {
                return;
            }

            try
            {     
                switch (operazione.Tipo) 
                {
                    case BackgroundOperationType.Mail:
                        await SendMail(cancellationToken, operazione);
                        break;

                    case BackgroundOperationType.Fake:
                        await FakeOperation(cancellationToken, operazione);
                        break;
                }

                await CompleteOperazione(operazione);
            }
            catch (Exception e)
            {
                await ImpostaErroreOperazione(operazione);
                this._logger.LogError(e, $"BackgroundServiceBO.DoWork Errore durante l'esecuzione dell'operazione con id {operazione.Id}");
            }

            await DoWork(cancellationToken);
        }

        private async Task SendMail(CancellationToken cancellationToken, Operation operazione)
        {
            var mails = JsonConvert.DeserializeObject<List<MailModel>>(operazione.Data);
            this._logger.LogInformation($"Sending mails");

            await this._mailService.SendEmail(mails, cancellationToken);
        }

        private async Task FakeOperation(CancellationToken cancellationToken, Operation operazione)
        {
            this._logger.LogInformation($"Fake operation: {operazione.Data}");

            for(var i = 1; i <= 10; i++) 
            {
                await Task.Delay(1000 * 3);
                await SetProgress(operazione, i * 10);
            }

            await CompleteOperazione(operazione);
        }

        private async Task SetProgress(Operation operazione, int progress)
        {
            operazione.Progress = progress;

            this._backgroundServiceUnitOfWork.OperationRepository.Update(operazione);
            await this._backgroundServiceUnitOfWork.SaveAsync();
        }

        private async Task CompleteOperazione(Operation operazione)
        {
            operazione.Completed = true;
            operazione.Progress = 100;

            this._backgroundServiceUnitOfWork.OperationRepository.Update(operazione);
            await this._backgroundServiceUnitOfWork.SaveAsync();
        }

        private async Task ImpostaErroreOperazione(Operation operazione)
        {
            operazione.Error = true;

            this._backgroundServiceUnitOfWork.OperationRepository.Update(operazione);
            await this._backgroundServiceUnitOfWork.SaveAsync();
        }

        public async Task CreateOperationMail(List<MailModel> mails, Guid utenteIdRequest)
        {
            var operation = CreateOperation(mails, BackgroundOperationType.Mail, utenteIdRequest);

            await InsertAsync(operation); 
        }

        public async Task CreateOperationMail(MailModel mails, Guid utenteIdRequest)
        {
            await CreateOperationMail(new List<MailModel>() { mails }, utenteIdRequest);
        }

        private Operation CreateOperation<T>(T data, BackgroundOperationType tipo, Guid utenteIdRequest)
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
