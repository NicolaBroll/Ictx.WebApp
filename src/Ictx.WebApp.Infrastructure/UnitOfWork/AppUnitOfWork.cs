using System;
using System.Threading.Tasks;
using Ictx.WebApp.Infrastructure.Data;
using Ictx.WebApp.Infrastructure.Repositories;

namespace Ictx.WebApp.Infrastructure.UnitOfWork
{
    public class AppUnitOfWork : IDisposable
    {
        private readonly AppDbContext       _context;

        // Repository.
        private DipendenteRepository                    _dipendenteRepository;
        private FoglioPresenzaRepository                _foglioPresenzaRepository;
        private FoglioPresenzaVpaRepository             _foglioPresenzaVpaRepository;
        private FoglioPresenzaDettaglioGiornoRepository _foglioPresenzaDettaglioGiornoRepository;

        public AppUnitOfWork(AppDbContext context)
        {
            this._context = context;
        }

        public DipendenteRepository DipendenteRepository
        {
            get
            {
                if (this._dipendenteRepository == null)
                    this._dipendenteRepository = new DipendenteRepository(_context);

                return _dipendenteRepository;
            }
        }

        public FoglioPresenzaRepository FoglioPresenzaRepository
        {
            get
            {
                if (this._foglioPresenzaRepository == null)
                    this._foglioPresenzaRepository = new FoglioPresenzaRepository(_context);

                return _foglioPresenzaRepository;
            }
        }

        public FoglioPresenzaVpaRepository FoglioPresenzaVpaRepository
        {
            get
            {
                if (this._foglioPresenzaVpaRepository == null)
                    this._foglioPresenzaVpaRepository = new FoglioPresenzaVpaRepository(_context);

                return _foglioPresenzaVpaRepository;
            }
        }

        public FoglioPresenzaDettaglioGiornoRepository FoglioPresenzaDettaglioGiornoRepository
        {
            get
            {
                if (this._foglioPresenzaDettaglioGiornoRepository == null)
                    this._foglioPresenzaDettaglioGiornoRepository = new FoglioPresenzaDettaglioGiornoRepository(_context);

                return _foglioPresenzaDettaglioGiornoRepository;
            }
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.DisposeAsync();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        //public override bool Equals(object obj)
        //{
        //    return obj is AppUnitOfWork work &&
        //           EqualityComparer<AppDbContext>.Default.Equals(_context, work._context);
        //}
    }
}