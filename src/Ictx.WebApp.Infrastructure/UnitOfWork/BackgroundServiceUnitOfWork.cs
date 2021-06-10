using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Ictx.WebApp.Application.AppUnitOfWork;
using Ictx.WebApp.Application.Repositories;
using Ictx.WebApp.Core.Entities;
using Ictx.WebApp.Core.Entities.Base;
using Ictx.WebApp.Infrastructure.Data.BackgroundService;
using Ictx.WebApp.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Ictx.WebApp.Infrastructure.UnitOfWork
{
    public class BackgroundServiceUnitOfWork : IBackgroundServiceUnitOfWork
    {
        private readonly BackgroundServiceDbContext _context;

        // Repository.
        private IGenericRepository<Operation> _operationRepository;

        public BackgroundServiceUnitOfWork(BackgroundServiceDbContext backgroundServiceDbContext)
        {
            this._context = backgroundServiceDbContext;
        }

        public IGenericRepository<Operation> OperationRepository
        {
            get
            {
                if (this._operationRepository == null)
                {
                    this._operationRepository = new GenericRepository<Operation, BackgroundServiceDbContext>(this._context);
                }

                return this._operationRepository;
            }
        }

        public async Task SaveAsync(CancellationToken cancellationToken = default)
        {
            var now = DateTime.Now;

            var entries = this._context.ChangeTracker
                .Entries()
                .Where(e => e.Entity is BaseEntity && (e.State == EntityState.Added || e.State == EntityState.Modified));

            foreach (var entityEntry in entries)
            {
                ((BaseEntity)entityEntry.Entity).Updated = now;

                if (entityEntry.State == EntityState.Added)
                {
                    ((BaseEntity)entityEntry.Entity).Inserted = now;
                }
            }

            await _context.SaveChangesAsync(cancellationToken);
        }

        private bool _disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    this._context.DisposeAsync();
                }
            }
            this._disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public BackgroundServiceDbContext GetContext()
        {
            return this._context;
        }

        //public override bool Equals(object obj)
        //{
        //    return obj is AppUnitOfWork work && EqualityComparer<AppDbContext>.Default.Equals(_context, work._context);
        //}
    }
}