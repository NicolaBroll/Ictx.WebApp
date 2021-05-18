using System;
using System.Linq;
using System.Threading.Tasks;
using Ictx.WebApp.Core.Entities;
using Ictx.WebApp.Core.Entities.Base;
using Ictx.WebApp.Core.Interfaces;
using Ictx.WebApp.Infrastructure.Data;
using Ictx.WebApp.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Ictx.WebApp.Infrastructure.UnitOfWork
{
    public class AppUnitOfWork : IAppUnitOfWork
    {
        private readonly AppDbContext _context;

        // Repository.
        private IGenericRepository<Dipendente> _dipendenteRepository;

        public AppUnitOfWork(AppDbContext context)
        {
            this._context = context;
        }

        public IGenericRepository<Dipendente> DipendenteRepository
        {
            get
            {
                if (this._dipendenteRepository == null) 
                {
                    this._dipendenteRepository = new GenericRepository<Dipendente, AppDbContext>(_context);
                }

                return _dipendenteRepository;
            }
        }

        public async Task SaveAsync()
        {
            var entries = this._context.ChangeTracker
                .Entries()
                .Where(e => e.Entity is BaseEntity && (e.State == EntityState.Added || e.State == EntityState.Modified));

            foreach (var entityEntry in entries)
            {
                ((BaseEntity)entityEntry.Entity).Updated = DateTime.Now;

                if (entityEntry.State == EntityState.Added)
                {
                    ((BaseEntity)entityEntry.Entity).Inserted = DateTime.Now;
                }
            }

            await _context.SaveChangesAsync();
        }

        private bool _disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    _context.DisposeAsync();
                }
            }
            this._disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public AppDbContext GetAppDbContext()
        {
            return this._context;
        }

        //public override bool Equals(object obj)
        //{
        //    return obj is AppUnitOfWork work && EqualityComparer<AppDbContext>.Default.Equals(_context, work._context);
        //}
    }
}