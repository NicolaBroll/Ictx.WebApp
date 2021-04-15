using System;
using System.Threading.Tasks;
using Ictx.WebApp.Core.Entities;
using Ictx.WebApp.Infrastructure.Data;
using Ictx.Framework.Repository;

namespace Ictx.WebApp.Infrastructure.UnitOfWork
{
    public class AppUnitOfWork
    {
        private readonly AppDbContext _context;

        // Repository.
        private GenericRepository<UfficioBase, AppDbContext>  _ufficioBaseRepository;
        private GenericRepository<Ufficio, AppDbContext>      _ufficioRepository;
        private GenericRepository<Impresa, AppDbContext>      _impresaRepository;
        private GenericRepository<Ditta, AppDbContext>        _dittaRepository;
        private GenericRepository<Dipendente, AppDbContext>   _dipendenteRepository;

        public AppUnitOfWork(AppDbContext context)
        {
            this._context = context;
        }

        public GenericRepository<UfficioBase, AppDbContext> UfficioBaseRepository
        {
            get
            {
                if (this._ufficioBaseRepository == null)
                    this._ufficioBaseRepository = new GenericRepository<UfficioBase, AppDbContext>(_context);

                return _ufficioBaseRepository;
            }
        }

        public GenericRepository<Ufficio, AppDbContext> UfficioRepository
        {
            get
            {
                if (this._ufficioRepository == null)
                    this._ufficioRepository = new GenericRepository<Ufficio, AppDbContext>(_context);

                return _ufficioRepository;
            }
        }

        public GenericRepository<Impresa, AppDbContext> ImpresaRepository
        {
            get
            {
                if (this._impresaRepository == null)
                    this._impresaRepository = new GenericRepository<Impresa, AppDbContext>(_context);

                return _impresaRepository;
            }
        }

        public GenericRepository<Ditta, AppDbContext> DittaRepository
        {
            get
            {
                if (this._dittaRepository == null)
                    this._dittaRepository = new GenericRepository<Ditta, AppDbContext>(_context);

                return _dittaRepository;
            }
        }

        public GenericRepository<Dipendente, AppDbContext> DipendenteRepository
        {
            get
            {
                if (this._dipendenteRepository == null)
                    this._dipendenteRepository = new GenericRepository<Dipendente, AppDbContext>(_context);

                return _dipendenteRepository;
            }
        }

        public async Task SaveAsync()
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