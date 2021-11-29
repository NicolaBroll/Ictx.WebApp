using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using MailKit.Net.Smtp;
using MimeKit;
using MimeKit.Text;
using Ictx.WebApp.Fwk.Models;

namespace Ictx.WebApp.Core.Services
{
    public interface IMailSettings
    {
        public string FromMailAddress { get; set; }
        public string FromName { get; set; }
        public string IpAddress { get; set; }
        public int Port { get; set; }
        public bool UseSsl { get; set; }
    }

    public class MailSettings : IMailSettings
    {
        public string FromMailAddress { get; set; }
        public string FromName { get; set; }
        public string IpAddress { get; set; }
        public int Port { get; set; }
        public bool UseSsl { get; set; }
    }

    public interface IMailService
    {
        Task SendEmail(List<MailModel> mails, CancellationToken cancellationToken = default);
        Task SendEmail(MailModel mail, CancellationToken cancellationToken = default);
    }

    public class MailService : IMailService
    {
        private readonly ILogger<MailService>   _logger;
        private readonly IMailSettings          _mailSettings;

        public MailService(ILogger<MailService> logger, IMailSettings mailSettings)
        {
            this._logger        = logger;
            this._mailSettings  = mailSettings;
        }

        public async Task SendEmail(List<MailModel> mails, CancellationToken cancellationToken = default)
        {
            try
            {
                using (var client = new SmtpClient())
                {
                    // For demo-purposes, accept all SSL certificates
                    //client.ServerCertificateValidationCallback = (_, _, _, _) => true;

                    await client.ConnectAsync(this._mailSettings.IpAddress, this._mailSettings.Port, this._mailSettings.UseSsl, cancellationToken);

                    foreach (var mail in mails)
                    {
                        if (cancellationToken.IsCancellationRequested) 
                        {
                            break;
                        }

                        await SendEmail(mail, client, cancellationToken);
                    }

                    await client.DisconnectAsync(true, cancellationToken);
                };
            }
            catch (Exception e)
            {
                this._logger.LogError(e, "MailService.SendEmail");
            }           
        }

        public async Task SendEmail(MailModel mail, CancellationToken cancellationToken = default)
        {
            await SendEmail(new List<MailModel> { mail }, cancellationToken);
        }

        private async Task SendEmail(MailModel mail, SmtpClient client, CancellationToken cancellationToken)
        {
            var message = new MimeMessage();

            message.From.Add(new MailboxAddress(this._mailSettings.FromName, this._mailSettings.FromMailAddress));
            message.To.Add(new MailboxAddress($"{mail.Nome} {mail.Cognome}", mail.Mail));
            message.Subject = mail.Subject;

            message.Body = new TextPart(TextFormat.Html)
            {
                Text = mail.Body
            };

            await client.SendAsync(message, cancellationToken: cancellationToken);            
        }
    }
}