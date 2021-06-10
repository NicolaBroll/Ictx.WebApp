﻿using System;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Ictx.WebApp.Application.UnitOfWork;
using Ictx.WebApp.Core.Entities.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Ictx.WebApp.Infrastructure.UnitOfWork
{
    public class UnitOfWorkBase<TDbContext> : IUnitOfWorkBase
        where TDbContext: DbContext
    {
        protected readonly TDbContext _context;
    
        public UnitOfWorkBase(TDbContext context)
        {
            this._context = context;
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
        private IDbContextTransaction _transaction;

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

        public void BeginTransaction()
        {
            if (this._transaction != null) 
            {
                DisposeTransaction();
            }

            this._transaction = this._context.Database.BeginTransaction(IsolationLevel.RepeatableRead);
        }

        public void CommitTransaction()
        {
            this._transaction.Commit();
            DisposeTransaction();
        }

        private void DisposeTransaction()
        {
            this._transaction.Dispose();
            this._transaction = null;
        }

        //public override bool Equals(object obj)
        //{
        //    return obj is AppUnitOfWork work && EqualityComparer<AppDbContext>.Default.Equals(_context, work._context);
        //}
    }
}