using Ictx.WebApp.Core.Interfaces;
using Ictx.WebApp.Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Ictx.WebApp.Infrastructure.Repositories
{
    public class GenericRepository<T, D> : IGenericRepository<T> where T : class where D : DbContext
    {
        internal D _context;
        internal DbSet<T> _dbSet;

        public GenericRepository(D context)
        {
            this._context = context;
            this._dbSet = context.Set<T>();
        }

        public async virtual Task<bool> AnyAsync(Expression<Func<T, bool>> filter = null, CancellationToken cancellationToken = default)
        {
            IQueryable<T> query = _dbSet;

            if (filter != null)
                return await query.AnyAsync(filter, cancellationToken);

            return await query.AnyAsync(cancellationToken);
        }

        public virtual IQueryable<T> QueryMany(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = "")
        {
            IQueryable<T> query = _dbSet;

            foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                query = query.Include(includeProperty);

            if (filter != null)
                query = query.Where(filter);

            if (orderBy != null)
                return orderBy(query);
            else
                return query;
        }

        public async virtual Task<IEnumerable<T>> ReadManyAsync(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = "",
            CancellationToken cancellationToken = default)
        {
            IQueryable<T> query = QueryMany(filter, orderBy, includeProperties);

            return await query.ToListAsync(cancellationToken: cancellationToken);
        }

        public async virtual Task<PageResult<T>> ReadManyPaginatedAsync(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = "",
            PaginationModel pagination = null,
            CancellationToken cancellationToken = default)
        {
            IQueryable<T> query = QueryMany(filter, orderBy, includeProperties);

            if(pagination is null) 
            {
                throw new ArgumentException("PaginationModel is null.");
            }

            // Pagination.
            var count = query.Count();
            var list = await query.Skip((pagination.Page - 1) * pagination.PageSize).Take(pagination.PageSize).ToListAsync(cancellationToken: cancellationToken);

            return new PageResult<T>(list, count);            
        }

        public async virtual Task<T> ReadAsync(object id, CancellationToken cancellationToken = default)
        {
            return await _dbSet.FindAsync(id, cancellationToken);
        }

        public async virtual Task InsertAsync(T entity, CancellationToken cancellationToken = default)
        {
            await _dbSet.AddAsync(entity, cancellationToken);
        }
        public async virtual Task InsertManyAsync(IEnumerable<T> entity, CancellationToken cancellationToken = default)
        {
            await _dbSet.AddRangeAsync(entity, cancellationToken);
        }

        public async virtual Task Delete(object id, CancellationToken cancellationToken = default)
        {
            T entityToDelete = await _dbSet.FindAsync(id, cancellationToken);
            Delete(entityToDelete);
        }
        public virtual void Delete(T entityToDelete)
        {
            if (_context.Entry(entityToDelete).State == EntityState.Detached)
                _dbSet.Attach(entityToDelete);

            _dbSet.Remove(entityToDelete);
        }

        public virtual void Update(T entityToUpdate)
        {
            _dbSet.Attach(entityToUpdate);
            _context.Entry(entityToUpdate).State = EntityState.Modified;
        }
    }
}
