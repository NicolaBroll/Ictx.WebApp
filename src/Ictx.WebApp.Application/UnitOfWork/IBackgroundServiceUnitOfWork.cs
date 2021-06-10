using System;
using System.Threading;
using System.Threading.Tasks;
using Ictx.WebApp.Application.Repositories;
using Ictx.WebApp.Core.Entities;

namespace Ictx.WebApp.Application.AppUnitOfWork
{
    public interface IBackgroundServiceUnitOfWork : IDisposable
    {
        IGenericRepository<Operation> OperationRepository { get; }

        Task SaveAsync(CancellationToken cancellationToken = default);
    }
}
