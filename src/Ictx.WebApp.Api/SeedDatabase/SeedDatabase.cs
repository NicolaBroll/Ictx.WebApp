using System;
using System.IO;
using System.Linq;
using Ictx.WebApp.Api.Database.SeedData;
using Ictx.WebApp.Infrastructure.Data;
using Ictx.WebApp.Infrastructure.UnitOfWork;

namespace Ictx.WebApp.Api.Database
{
    public class SeedDatabase
    {
        private readonly AppUnitOfWork _appUnitOfWork;
        private readonly AppDbContext _context;
        private readonly string _seedDataDirectory;
        private readonly Random _random;

        public SeedDatabase(AppDbContext context)
        {
            this._appUnitOfWork = new AppUnitOfWork(context);
            this._context = context;
            this._seedDataDirectory = Path.Combine(Directory.GetCurrentDirectory(), "SeedData");
            this._random = new Random();
        }

        public async void Initialize()
        {
            _context.Database.EnsureCreated();

            // Uffici base.
            if(!_context.UfficioBase.Any())
            {
                // Ufficio base.
                var seedComune = new SeedComuni(_appUnitOfWork);
                await seedComune.Popola();

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

                // Dipendente.
                var seedDipendente = new SeedDipendente(_appUnitOfWork);
                await seedDipendente.Popola();
            }

            _context.Dispose();
        }
    }
}
