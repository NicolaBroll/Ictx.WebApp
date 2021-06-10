using Ictx.WebApp.Core.Entities;
using Ictx.WebApp.Core.Models;
using System.Threading.Tasks;

namespace Ictx.WebApp.Application.Repositories
{
    public interface IOperationRepository : IGenericRepository<Operation>
    {
        Task<Operation> GetNextOperation();

    }
}
