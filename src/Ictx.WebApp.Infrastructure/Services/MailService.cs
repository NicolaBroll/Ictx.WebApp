using MailKit.Net.Smtp;
using MimeKit;
using MimeKit.Text;
using System.Collections.Generic;
using Ictx.WebApp.Application.Common;
using Ictx.WebApp.Core.Models;
using Ictx.WebApp.Core.Interfaces;
using System.Threading.Tasks;

namespace Ictx.WebApp.Infrastructure.Services
{
    public class MailService: IMailService
    {
        private readonly IMailSettings _mailSettings;

        public MailService(IMailSettings mailSettings)
        {
            this._mailSettings = mailSettings;
        }

        public async Task SendEmail(List<MailModel> mails)
        {
            using (var client = new SmtpClient())
            {
                // For demo-purposes, accept all SSL certificates
                //client.ServerCertificateValidationCallback = (_, _, _, _) => true;

                await client.ConnectAsync(this._mailSettings.IpAddress, this._mailSettings.Port, this._mailSettings.UseSsl);

                foreach (var mail in mails)
                {
                    await SendEmail(mail, client);
                }

                await client.DisconnectAsync(true);
            };
        }

        public async Task SendEmail(MailModel mail)
        {
            await SendEmail(new List<MailModel> { mail });
        }

        private async Task SendEmail(MailModel mail, SmtpClient client)
        {
            var message = new MimeMessage();

            message.From.Add(new MailboxAddress(this._mailSettings.FromName, this._mailSettings.FromMailAddress));
            message.To.Add(new MailboxAddress($"{mail.Nome} {mail.Cognome}", mail.Mail));
            message.Subject = mail.Subject;

            message.Body = new TextPart(TextFormat.Html)
            {
                Text = mail.Body
            };

            await client.SendAsync(message);            
        }
    }
}