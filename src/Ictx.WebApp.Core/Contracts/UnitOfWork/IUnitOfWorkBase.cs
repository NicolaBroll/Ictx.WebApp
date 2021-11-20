using System;
using System.Data;
using System.Threading;
using System.Threading.Tasks;

namespace Ictx.WebApp.Core.Contracts.UnitOfWork
{
    public interface IUnitOfWorkBase : IDisposable
    {
        Task SaveAsync(CancellationToken cancellationToken = default);
    }
}
