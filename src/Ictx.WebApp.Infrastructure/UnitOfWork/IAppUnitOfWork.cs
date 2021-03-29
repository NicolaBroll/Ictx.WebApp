using System;
using System.Threading.Tasks;
using Ictx.WebApp.Core.Entities;
using Ictx.WebApp.Infrastructure.Repositories;

namespace Ictx.WebApp.Infrastructure.UnitOfWork
{
    public interface IAppUnitOfWork: IDisposable
    {
        GenericRepository<Dipendente> DipendenteRepository { get; }
        GenericRepository<Ditta> DittaRepository { get; }
        GenericRepository<Impresa> ImpresaRepository { get; }
        GenericRepository<UfficioBase> UfficioBaseRepository { get; }
        GenericRepository<Ufficio> UfficioRepository { get; }

        Task SaveAsync();
    }
}