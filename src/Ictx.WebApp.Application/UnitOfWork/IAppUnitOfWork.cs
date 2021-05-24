using System;
using System.Threading;
using System.Threading.Tasks;
using Ictx.WebApp.Application.Repositories;
using Ictx.WebApp.Core.Entities;

namespace Ictx.WebApp.Application.AppUnitOfWork
{
    public interface IAppUnitOfWork : IDisposable
    {
        IGenericRepository<Dipendente> DipendenteRepository { get; }

        Task SaveAsync(CancellationToken cancellationToken = default);
    }
}
