﻿using System;
using System.Threading.Tasks;
using Ictx.WebApp.Core.Entities;
using Ictx.WebApp.Infrastructure.Data;
using Ictx.WebApp.Infrastructure.Repositories;

namespace Ictx.WebApp.Infrastructure.UnitOfWork
{
    public interface IAppUnitOfWork : IDisposable
    {
        IGenericRepository<Dipendente> DipendenteRepository { get; }
        IGenericRepository<Ditta> DittaRepository { get; }
        IGenericRepository<Impresa> ImpresaRepository { get; }
        IGenericRepository<UfficioBase> UfficioBaseRepository { get; }
        IGenericRepository<Ufficio> UfficioRepository { get; }

        Task SaveAsync();
        AppDbContext GetAppDbContext();
    }

    public class AppUnitOfWork : IAppUnitOfWork
    {
        private readonly AppDbContext _context;

        // Repository.
        private IGenericRepository<UfficioBase> _ufficioBaseRepository;
        private IGenericRepository<Ufficio> _ufficioRepository;
        private IGenericRepository<Impresa> _impresaRepository;
        private IGenericRepository<Ditta> _dittaRepository;
        private IGenericRepository<Dipendente> _dipendenteRepository;

        public AppUnitOfWork(AppDbContext context)
        {
            this._context = context;
        }

        public IGenericRepository<UfficioBase> UfficioBaseRepository
        {
            get
            {
                if (this._ufficioBaseRepository == null)
                    this._ufficioBaseRepository = new GenericRepository<UfficioBase, AppDbContext>(_context);

                return _ufficioBaseRepository;
            }
        }

        public IGenericRepository<Ufficio> UfficioRepository
        {
            get
            {
                if (this._ufficioRepository == null)
                    this._ufficioRepository = new GenericRepository<Ufficio, AppDbContext>(_context);

                return _ufficioRepository;
            }
        }

        public IGenericRepository<Impresa> ImpresaRepository
        {
            get
            {
                if (this._impresaRepository == null)
                    this._impresaRepository = new GenericRepository<Impresa, AppDbContext>(_context);

                return _impresaRepository;
            }
        }

        public IGenericRepository<Ditta> DittaRepository
        {
            get
            {
                if (this._dittaRepository == null)
                    this._dittaRepository = new GenericRepository<Ditta, AppDbContext>(_context);

                return _dittaRepository;
            }
        }

        public IGenericRepository<Dipendente> DipendenteRepository
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