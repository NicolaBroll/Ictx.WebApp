using System.Threading.Tasks;

namespace Ictx.WebApp.Application.Services
{
    public interface IBackgroundService
    {
        Task DoWork();
    }
}
