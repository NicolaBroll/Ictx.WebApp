using Ictx.WebApp.Core.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Ictx.WebApp.Core.Contracts.Services
{
    public interface IMailService
    {
        Task SendEmail(List<MailModel> mails, CancellationToken cancellationToken = default);
        Task SendEmail(MailModel mail, CancellationToken cancellationToken = default);
    }
}
