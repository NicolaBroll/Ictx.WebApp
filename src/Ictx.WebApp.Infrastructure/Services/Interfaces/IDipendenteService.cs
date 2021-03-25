using Ictx.WebApp.Core.Entities;
using Ictx.WebApp.Core.Models;
using LanguageExt.Common;
using System.Threading.Tasks;

namespace Ictx.WebApp.Infrastructure.Services.Interfaces
{
    public interface IDipendenteService
    {
        Task<Result<Dipendente>> DeleteAsync(int id);
        Task<Result<Dipendente>> GetByIdAsync(int id);
        Task<PageResult<Dipendente>> GetListAsync(DipendenteListFilter filter);
        Task<Result<Dipendente>> InsertAsync(Dipendente model);
        Task<Result<Dipendente>> SaveAsync(int id, Dipendente model);
    }
}