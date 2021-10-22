using System;
using System.Threading;
using System.Threading.Tasks;
using Ictx.WebApp.Application.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace Ictx.WebApp.Infrastructure.UnitOfWork
{
    public class UnitOfWorkBase<TDbContext> : IUnitOfWorkBase
        where TDbContext: DbContext
    {
        protected readonly TDbContext   _context;
        private bool                    _disposed = false;

        public UnitOfWorkBase(TDbContext context)
        {
            this._context = context;
        }

        public async Task SaveAsync(CancellationToken cancellationToken = default)
        {
            await _context.SaveChangesAsync(cancellationToken);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this._disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}