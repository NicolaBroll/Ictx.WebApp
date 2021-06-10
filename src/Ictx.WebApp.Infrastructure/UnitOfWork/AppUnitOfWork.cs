using Ictx.WebApp.Application.UnitOfWork;
using Ictx.WebApp.Application.Repositories;
using Ictx.WebApp.Core.Entities;
using Ictx.WebApp.Infrastructure.Data.App;
using Ictx.WebApp.Infrastructure.Repositories;

namespace Ictx.WebApp.Infrastructure.UnitOfWork
{
    public class AppUnitOfWork : UnitOfWorkBase<AppDbContext>, IAppUnitOfWork
    {
        // Repository.
        private IGenericRepository<Dipendente> _dipendenteRepository;

        public AppUnitOfWork(AppDbContext appDbContext) : base(appDbContext)
        { }

        public IGenericRepository<Dipendente> DipendenteRepository
        {
            get
            {
                if (this._dipendenteRepository == null)
                {
                    this._dipendenteRepository = new GenericRepository<Dipendente, AppDbContext>(this._context);
                }

                return this._dipendenteRepository;
            }
        }           
    }
}