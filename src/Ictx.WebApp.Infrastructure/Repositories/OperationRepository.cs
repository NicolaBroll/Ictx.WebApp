using Ictx.WebApp.Application.Repositories;
using Ictx.WebApp.Core.Entities;
using Ictx.WebApp.Infrastructure.Data.BackgroundService;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Ictx.WebApp.Infrastructure.Repositories
{
    public class OperationRepository: GenericRepository<Operation, BackgroundServiceDbContext>, IOperationRepository
    {
        public OperationRepository(BackgroundServiceDbContext backgroundServiceDbContext) : base(backgroundServiceDbContext)
        { }

        public async Task<Operation> GetNextOperation()
        {
            var nextOperation = await QueryMany(
                filter: x => !x.Started && !x.Errore,
                orderBy: x => x.OrderBy(order => order.Inserted)).FirstOrDefaultAsync();

            return nextOperation;
        }
    }
}
