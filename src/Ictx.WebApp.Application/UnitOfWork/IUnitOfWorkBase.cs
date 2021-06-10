using System;
using System.Threading;
using System.Threading.Tasks;

namespace Ictx.WebApp.Application.UnitOfWork
{
    public interface IUnitOfWorkBase : IDisposable
    {
        Task SaveAsync(CancellationToken cancellationToken = default);
        Task BeginTransactionAsync();
        Task CommitTransactionAsync(bool dispose);
        Task DisposeTransactionAsync();
    }
}
