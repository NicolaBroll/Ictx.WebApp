using System;
using System.Threading.Tasks;
using Ictx.WebApp.Core.Entities;
using Ictx.Framework.Repository.Interfaces;
using Ictx.WebApp.Infrastructure.Data;

namespace Ictx.WebApp.Infrastructure.UnitOfWork
{
    public interface IAppUnitOfWork: IDisposable
    {
        IGenericRepository<Dipendente> DipendenteRepository { get; }
        IGenericRepository<Ditta> DittaRepository { get; }
        IGenericRepository<Impresa> ImpresaRepository { get; }
        IGenericRepository<UfficioBase> UfficioBaseRepository { get; }
        IGenericRepository<Ufficio> UfficioRepository { get; }

        Task SaveAsync();

        AppDbContext GetAppDbContext();
    }
}