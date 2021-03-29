using System;
using System.Threading.Tasks;
using Ictx.WebApp.Core.Entities;
using Ictx.WebApp.Infrastructure.Data;
using Ictx.WebApp.Infrastructure.Repositories;

namespace Ictx.WebApp.Infrastructure.UnitOfWork
{
    public class AppUnitOfWork : IAppUnitOfWork
    {
        private readonly AppDbContext _context;

        // Repository.
        private GenericRepository<UfficioBase>  _ufficioBaseRepository;
        private GenericRepository<Ufficio>      _ufficioRepository;
        private GenericRepository<Impresa>      _impresaRepository;
        private GenericRepository<Ditta>        _dittaRepository;
        private GenericRepository<Dipendente>   _dipendenteRepository;

        public AppUnitOfWork(AppDbContext context)
        {
            this._context = context;
        }

        public GenericRepository<UfficioBase> UfficioBaseRepository
        {
            get
            {
                if (this._ufficioBaseRepository == null)
                    this._ufficioBaseRepository = new GenericRepository<UfficioBase>(_context);

                return _ufficioBaseRepository;
            }
        }

        public GenericRepository<Ufficio> UfficioRepository
        {
            get
            {
                if (this._ufficioRepository == null)
                    this._ufficioRepository = new GenericRepository<Ufficio>(_context);

                return _ufficioRepository;
            }
        }

        public GenericRepository<Impresa> ImpresaRepository
        {
            get
            {
                if (this._impresaRepository == null)
                    this._impresaRepository = new GenericRepository<Impresa>(_context);

                return _impresaRepository;
            }
        }

        public GenericRepository<Ditta> DittaRepository
        {
            get
            {
                if (this._dittaRepository == null)
                    this._dittaRepository = new GenericRepository<Ditta>(_context);

                return _dittaRepository;
            }
        }

        public GenericRepository<Dipendente> DipendenteRepository
        {
            get
            {
                if (this._dipendenteRepository == null)
                    this._dipendenteRepository = new GenericRepository<Dipendente>(_context);

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