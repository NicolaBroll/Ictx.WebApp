using Ictx.WebApp.Core.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace Ictx.WebApp.Application.Repositories
{
    public interface IOperationRepository : IGenericRepository<Operation>
    {
        Task<Operation> GetNextOperation();
        Task<Operation> GetAndStartNextOperazione(CancellationToken cancellationToken);
    }
}
