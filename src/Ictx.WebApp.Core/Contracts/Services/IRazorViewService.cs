using System.Threading.Tasks;

namespace Ictx.WebApp.Core.Contracts.Services
{
    public interface IRazorViewService
    {
        Task<string> RenderViewToStringAsync<TModel>(string viewName, TModel model);
    }
}
