using Ictx.WebApp.Application.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Ictx.WebApp.Application.Services
{
    public interface IMailService
    {
        Task SendEmail(List<MailModel> mails, CancellationToken cancellationToken = default);
        Task SendEmail(MailModel mail, CancellationToken cancellationToken = default);
    }
}
