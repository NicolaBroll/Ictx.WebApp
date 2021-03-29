using Ictx.WebApp.Api.Database.SeedData;
using Ictx.WebApp.Infrastructure.Data;
using Ictx.WebApp.Infrastructure.UnitOfWork;
using System;
using System.Threading.Tasks;

namespace Ictx.WebApp.Api.Database
{
    public class SeedDatabase: IDisposable
    {
        private readonly AppUnitOfWork _appUnitOfWork;
        private readonly AppDbContext _context;

        public SeedDatabase(AppDbContext context)
        {
            this._appUnitOfWork = new AppUnitOfWork(context);
            this._context = context;
        }

        public async Task Initialize()
        {
            _context.Database.EnsureCreated();

            // Uffici base.
            if(!(await _appUnitOfWork.UfficioBaseRepository.AnyAsync()))
            {
                // Ufficio base.
                var seedUfficioBase = new SeedUfficioBase(_appUnitOfWork);
                await seedUfficioBase.Popola();

                // Ufficio.
                var seedUffici = new SeedUfficio(_appUnitOfWork);
                await seedUffici.Popola();

                // Impresa.
                var seedImpresa = new SeedImpresa(_appUnitOfWork);
                await seedImpresa.Popola();

                // Ditta.
                var seedDitta = new SeedDitta(_appUnitOfWork);
                await seedDitta.Popola();
            }
        }

        public void Dispose()
        {
            this._appUnitOfWork.Dispose();
        }
    }
}
