using Ictx.WebApp.Core.Contracts.Repositories;
using Ictx.WebApp.Core.Entities;

namespace Ictx.WebApp.Core.Contracts.UnitOfWork
{
    public interface IAppUnitOfWork : IUnitOfWorkBase
    {
        IGenericRepository<Dipendente> DipendenteRepository { get; }
    }
}
