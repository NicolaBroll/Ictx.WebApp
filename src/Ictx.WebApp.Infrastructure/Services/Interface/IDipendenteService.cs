using Ictx.WebApp.Core.Entities;
using System.Threading.Tasks;
using static Ictx.WebApp.Core.Models.PaginationModel;

namespace Ictx.WebApp.Infrastructure.Services.Interface
{
    public interface IDipendenteService
    {
        Task<PageResult<Dipendente>> GetListAsync(PaginationFilterModel paginationFilterModel);

        Task<Dipendente> GetByIdAsync(int id);

        Task<Dipendente> InsertAsync(Dipendente dipendente);

        Task<Dipendente> SaveAsync(int id, Dipendente dipendente);

        Task<bool> DeleteAsync(int id);
    }
}
