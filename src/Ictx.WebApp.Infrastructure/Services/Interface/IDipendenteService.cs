using Ictx.WebApp.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ictx.WebApp.Infrastructure.Services.Interface
{
    public interface IDipendenteService
    {
        Task<IEnumerable<Dipendente>> GetListAsync();

        Task<Dipendente> GetByIdAsync(int id);

        Task<Dipendente> InsertAsync(Dipendente dipendente);

        Task<Dipendente> SaveAsync(int id, Dipendente dipendente);

        Task<bool> DeleteAsync(int id);
    }
}
