using Ictx.WebApp.Application.Repositories;
using Ictx.WebApp.Core.Entities;
using Ictx.WebApp.Infrastructure.Data.BackgroundService;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;

namespace Ictx.WebApp.Infrastructure.Repositories
{
    public class OperationRepository: GenericRepository<Operation, BackgroundServiceDbContext>, IOperationRepository
    {
        public OperationRepository(BackgroundServiceDbContext backgroundServiceDbContext) : base(backgroundServiceDbContext)
        { }

        public async Task<Operation> GetNextOperation()
        {
            var nextOperation = await QueryMany(
                filter: x => !x.Started && !x.Error,
                orderBy: x => x.OrderBy(order => order.Inserted)).FirstOrDefaultAsync();

            return nextOperation;
        }

        public async Task<Operation> GetAndStartNextOperazione(CancellationToken cancellationToken)
        {
            Operation operazione = null;

            using (var tran = await this._context.Database.BeginTransactionAsync())
            {
                operazione = await GetNextOperation();

                if (cancellationToken.IsCancellationRequested || operazione is null || operazione.Started)
                {
                    return null;
                }

                // Avvio l'operazione.
                operazione.Started = true;

                Update(operazione);

                await this._context.SaveChangesAsync();
            }

            return operazione;
        }
    }
}
