using Ictx.WebApp.Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Ictx.WebApp.Infrastructure.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        Task<bool> AnyAsync(Expression<Func<T, bool>> filter = null);
        void Delete(object id);
        void Delete(T entityToDelete);
        Task InsertAsync(T entity);
        Task InsertManyAsync(IEnumerable<T> entity);
        IQueryable<T> QueryMany(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties = "");
        Task<T> ReadAsync(object id);
        Task<IEnumerable<T>> ReadManyAsync(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties = "");
        void Update(T entityToUpdate);
    }

    public class GenericRepository<T, D> : IGenericRepository<T> where T : class where D : DbContext
    {
        internal D _context;
        internal DbSet<T> _dbSet;

        public GenericRepository(D context)
        {
            this._context = context;
            this._dbSet = context.Set<T>();
        }

        public async virtual Task<bool> AnyAsync(Expression<Func<T, bool>> filter = null)
        {
            IQueryable<T> query = _dbSet;

            if (filter != null)
                return await query.AnyAsync(filter);

            return await query.AnyAsync();
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
            string includeProperties = "")
        {
            IQueryable<T> query = QueryMany(filter, orderBy, includeProperties);

            return await query.ToListAsync();
        }

        public async virtual Task<T> ReadAsync(object id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async virtual Task InsertAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
        }
        public async virtual Task InsertManyAsync(IEnumerable<T> entity)
        {
            await _dbSet.AddRangeAsync(entity);
        }

        public virtual void Delete(object id)
        {
            T entityToDelete = _dbSet.Find(id);
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
