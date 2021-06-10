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
            var operazione = await GetAndStartNextOperazione(cancellationToken);

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
            var mail = JsonConvert.DeserializeObject<MailModel>(operazione.Data);
            this._logger.LogInformation($"Sending mail to: {mail.Mail}");

            await this._mailService.SendEmail(mail, cancellationToken);
        }

        private async Task FakeOperation(CancellationToken cancellationToken, Operation operazione)
        {
            this._logger.LogInformation($"Fake operation: {operazione.Data}");

            await CompleteOperazione(operazione);
        }

        private async Task CompleteOperazione(Operation operazione)
        {
            operazione.Completed = true;

            this._backgroundServiceUnitOfWork.OperationRepository.Update(operazione);
            await this._backgroundServiceUnitOfWork.SaveAsync();
        }

        private async Task ImpostaErroreOperazione(Operation operazione)
        {
            operazione.Errore = true;

            this._backgroundServiceUnitOfWork.OperationRepository.Update(operazione);
            await this._backgroundServiceUnitOfWork.SaveAsync();
        }

        private async Task<Operation> GetAndStartNextOperazione(CancellationToken cancellationToken)
        {
            // Inizia la transaction.
            await this._backgroundServiceUnitOfWork.BeginTransactionAsync();

            var operazione = await this._backgroundServiceUnitOfWork.OperationRepository.GetNextOperation();

            if (cancellationToken.IsCancellationRequested || operazione is null || operazione.Started)
            {
                return null;
            }

            // Avvio l'operazione.
            operazione.Started = true;

            this._backgroundServiceUnitOfWork.OperationRepository.Update(operazione);
            await this._backgroundServiceUnitOfWork.SaveAsync();

            // Committo e chiudo la transaction.
            await this._backgroundServiceUnitOfWork.CommitTransactionAsync(true);

            return operazione;
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
