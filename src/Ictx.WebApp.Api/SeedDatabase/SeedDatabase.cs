using Ictx.WebApp.Api.Database.SeedData;
using Ictx.WebApp.Infrastructure.Data;
using Ictx.WebApp.Infrastructure.UnitOfWork;

namespace Ictx.WebApp.Api.Database
{
    public class SeedDatabase
    {
        private readonly AppUnitOfWork _appUnitOfWork;
        private readonly AppDbContext _context;

        public SeedDatabase(AppDbContext context)
        {
            this._appUnitOfWork = new AppUnitOfWork(context);
            this._context = context;
        }

        public async void Initialize()
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

            _context.Dispose();
        }
    }
}
