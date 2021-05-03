using Ictx.WebApp.Core.Entities;
using Ictx.WebApp.Core.Models;
using System.Threading.Tasks;

namespace Ictx.WebApp.Core.Interfaces
{
    public interface IDipendenteService
    {
        Task<OperationResult<bool>> DeleteAsync(int key);
        Task<OperationResult<Dipendente>> InsertAsync(Dipendente value);
        Task<OperationResult<Dipendente>> ReadAsync(int key);
        Task<PageResult<Dipendente>> ReadManyAsync(DipendenteListFilter filter);
        Task<OperationResult<Dipendente>> SaveAsync(int key, Dipendente value);
    }
}