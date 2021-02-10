using System;
using System.Threading.Tasks;
using Ictx.WebApp.Infrastructure.Data;
using Ictx.WebApp.Infrastructure.Repositories;

namespace Ictx.WebApp.Infrastructure.UnitOfWork
{
    public class AppUnitOfWork : IDisposable
    {
        private readonly AppDbContext _context;

        // Repository.
        private ComuneRepository _comuneRepository;
        private UfficioBaseRepository _ufficioBaseRepository;
        private UfficioRepository _ufficioRepository;
        private ImpresaRepository _impresaRepository;
        private DittaRepository _dittaRepository;
        private DipendenteRepository _dipendenteRepository;

        public AppUnitOfWork(AppDbContext context)
        {
            this._context = context;
        }

        public ComuneRepository ComuneRepository
        {
            get
            {
                if (this._comuneRepository == null)
                    this._comuneRepository = new ComuneRepository(_context);

                return _comuneRepository;
            }
        }

        public UfficioBaseRepository UfficioBaseRepository
        {
            get
            {
                if (this._ufficioBaseRepository == null)
                    this._ufficioBaseRepository = new UfficioBaseRepository(_context);

                return _ufficioBaseRepository;
            }
        }

        public UfficioRepository UfficioRepository
        {
            get
            {
                if (this._ufficioRepository == null)
                    this._ufficioRepository = new UfficioRepository(_context);

                return _ufficioRepository;
            }
        }

        public ImpresaRepository ImpresaRepository
        {
            get
            {
                if (this._impresaRepository == null)
                    this._impresaRepository = new ImpresaRepository(_context);

                return _impresaRepository;
            }
        }

        public DittaRepository DittaRepository
        {
            get
            {
                if (this._dittaRepository == null)
                    this._dittaRepository = new DittaRepository(_context);

                return _dittaRepository;
            }
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