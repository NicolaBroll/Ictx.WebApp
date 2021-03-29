using Ictx.WebApp.Core.Entities;
using LanguageExt.Common;
using System.Threading.Tasks;

namespace Ictx.WebApp.Infrastructure.Services.Interfaces
{
    public interface IDittaService
    {
        Task<Result<Ditta>> GetByIdAsync(int id);
    }
}