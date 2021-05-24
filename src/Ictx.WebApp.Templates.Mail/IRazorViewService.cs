using System.Threading.Tasks;

namespace Ictx.WebApp.Templates.Mail
{
    public interface IRazorViewService
    {
        Task<string> RenderViewToStringAsync<TModel>(string viewName, TModel model);
    }
}
