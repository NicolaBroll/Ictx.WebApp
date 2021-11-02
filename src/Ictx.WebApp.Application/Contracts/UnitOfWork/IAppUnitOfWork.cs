using Ictx.WebApp.Application.Contracts.Repositories;
using Ictx.WebApp.Core.Entities;

namespace Ictx.WebApp.Application.Contracts.UnitOfWork
{
    public interface IAppUnitOfWork : IUnitOfWorkBase
    {
        IGenericRepository<Dipendente> DipendenteRepository { get; }
    }
}
