using System;
using System.Threading.Tasks;
using Ictx.WebApp.Api.Data.SeedData;
using Ictx.WebApp.Infrastructure.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace Ictx.WebApp.Api.Data
{
    public class SeedDatabase: IDisposable
    {
        private readonly IAppUnitOfWork _appUnitOfWork;

        public SeedDatabase(IAppUnitOfWork appUnitOfWork)
        {
            this._appUnitOfWork = appUnitOfWork;
        }

        public async Task Initialize()
        {
            // Crea il db in base al dtc e non considerando le migrations.
            // _context.Database.EnsureCreated();

            // Eseguo le migrations.
            this._appUnitOfWork.GetAppDbContext().Database.Migrate();

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
