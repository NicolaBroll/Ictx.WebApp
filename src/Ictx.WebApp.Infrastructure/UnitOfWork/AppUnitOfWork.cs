﻿using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Ictx.WebApp.Application.AppUnitOfWork;
using Ictx.WebApp.Application.Repositories;
using Ictx.WebApp.Core.Entities;
using Ictx.WebApp.Core.Entities.Base;
using Ictx.WebApp.Infrastructure.Data.App;
using Ictx.WebApp.Infrastructure.Data.BackgroundService;
using Ictx.WebApp.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Ictx.WebApp.Infrastructure.UnitOfWork
{
    public class AppUnitOfWork : IAppUnitOfWork
    {
        private readonly AppDbContext _appDbContext;
        private readonly BackgroundServiceDbContext _backgroundServiceDbContext;

        // Repository.
        private IGenericRepository<Dipendente> _dipendenteRepository;
        private IGenericRepository<Operation> _operationRepository;

        public AppUnitOfWork(AppDbContext appDbContext, BackgroundServiceDbContext backgroundServiceDbContext)
        {
            this._appDbContext = appDbContext;
            this._backgroundServiceDbContext = backgroundServiceDbContext;
        }

        public IGenericRepository<Dipendente> DipendenteRepository
        {
            get
            {
                if (this._dipendenteRepository == null)
                {
                    this._dipendenteRepository = new GenericRepository<Dipendente, AppDbContext>(this._appDbContext);
                }

                return this._dipendenteRepository;
            }
        }

        public IGenericRepository<Operation> OperationRepository
        {
            get
            {
                if (this._operationRepository == null)
                {
                    this._operationRepository = new GenericRepository<Operation, BackgroundServiceDbContext>(this._backgroundServiceDbContext);
                }

                return this._operationRepository;
            }
        }

        public async Task SaveAsync(CancellationToken cancellationToken = default)
        {
            var now = DateTime.Now;

            var entries = this._appDbContext.ChangeTracker
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

            await _appDbContext.SaveChangesAsync(cancellationToken);
        }

        private bool _disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    _appDbContext.DisposeAsync();
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
            return this._appDbContext;
        }

        //public override bool Equals(object obj)
        //{
        //    return obj is AppUnitOfWork work && EqualityComparer<AppDbContext>.Default.Equals(_context, work._context);
        //}
    }
}