using Ictx.WebApp.Application.Repositories;
using Ictx.WebApp.Core.Entities;

namespace Ictx.WebApp.Application.UnitOfWork
{
    public interface IAppUnitOfWork : IUnitOfWorkBase
    {
        IGenericRepository<Dipendente> DipendenteRepository { get; }
    }
}
