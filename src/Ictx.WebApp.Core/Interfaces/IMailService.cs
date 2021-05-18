using Ictx.WebApp.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ictx.WebApp.Core.Interfaces
{
    public interface IMailService
    {
        Task SendEmail(List<MailModel> mails);
        Task SendEmail(MailModel mail);
    }
}
