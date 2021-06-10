using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Ictx.WebApp.Templates.Mail;
using Ictx.WebApp.Application.Models;
using Ictx.WebApp.Core.Models;

namespace Ictx.WebApp.Application.BO
{
    public class MailBO : BaseBO<DipendenteEmailTemplate, int, PaginationModel>
    {
        private readonly IRazorViewService      _razorViewService;
        private readonly BackgroundServiceBO    _backgroundServiceBO;

        public MailBO(ILogger<MailBO> logger,
            IRazorViewService razorViewService,
            BackgroundServiceBO backgroundServiceBO): base(logger, null)
        {
            this._razorViewService      = razorViewService;
            this._backgroundServiceBO   = backgroundServiceBO;
        }

        /// <summary>
        /// Crea e invia una mail.
        /// </summary>
        /// <param name="value">Modello contenente i dati della nuova mail.</param>
        protected override async Task<OperationResult<DipendenteEmailTemplate>> InsertViewAsync(DipendenteEmailTemplate value, CancellationToken cancellationToken)
        {
            var utenteIdRequest = System.Guid.NewGuid();

            var utente = new Utente
            {
                Id = System.Guid.NewGuid(),
                Nome = "Nicola",
                Cognome = "Broll",
                Email = "nbroll@gmail.com"
            };

            string body = await _razorViewService.RenderViewToStringAsync("/Views/Emails/Prova.cshtml", value);

            var mail = new MailModel
            {
                Nome = utente.Nome,
                Cognome = utente.Cognome,
                Mail = utente.Email,
                Subject = "Prova",
                Body = body
            };

            await this._backgroundServiceBO.CreateOperationMail(mail, utenteIdRequest);

            return new OperationResult<DipendenteEmailTemplate>(value);
        }
    }
}
