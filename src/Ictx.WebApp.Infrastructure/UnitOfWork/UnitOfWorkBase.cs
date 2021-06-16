using System;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using Ictx.WebApp.Application.UnitOfWork;
using Ictx.WebApp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Ictx.WebApp.Infrastructure.UnitOfWork
{
    public class UnitOfWorkBase<TDbContext> : IUnitOfWorkBase
        where TDbContext: DbContextBase
    {
        protected readonly TDbContext _context;
    
        public UnitOfWorkBase(TDbContext context)
        {
            this._context = context;
        }

        public async Task SaveAsync(CancellationToken cancellationToken = default)
        {
            await _context.SaveChangesAsync(cancellationToken);
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

        public TDbContext GetAppDbContext()
        {
            return this._context;
        }

        public IDbConnection GetConnection()
        {
            return this._context.Database.GetDbConnection();
        }
    }
}