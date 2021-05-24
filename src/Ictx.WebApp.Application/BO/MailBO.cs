using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Ictx.WebApp.Application.Services;
using Ictx.WebApp.Templates.Mail;
using Ictx.WebApp.Application.Models;
using Ictx.WebApp.Core.Models;

namespace Ictx.WebApp.Application.BO
{
    public class MailBO : BaseBO<DipendenteEmailTemplate, int, PaginationModel>
    {
        private readonly IRazorViewService  _razorViewService;
        private readonly IMailService       _mailService;

        public MailBO(ILogger<MailBO> logger, IRazorViewService razorViewService, IMailService mailService): base(logger, null)
        {
            this._razorViewService  = razorViewService;
            this._mailService       = mailService;
        }

        /// <summary>
        /// Crea e invia una mail.
        /// </summary>
        /// <param name="value">Modello contenente i dati della nuova mail.</param>
        protected override async Task<OperationResult<DipendenteEmailTemplate>> InsertViewAsync(DipendenteEmailTemplate value, CancellationToken cancellationToken)
        {
            var utente = new Utente
            {
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

            await this._mailService.SendEmail(mail, cancellationToken);

            return new OperationResult<DipendenteEmailTemplate>(value);
        }
    }
}
