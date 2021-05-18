using Ictx.WebApp.Core.Entities;
using System;
using System.Threading.Tasks;

namespace Ictx.WebApp.Core.Interfaces
{
    public interface IAppUnitOfWork : IDisposable
    {
        IGenericRepository<Dipendente> DipendenteRepository { get; }

        Task SaveAsync();
    }
}
