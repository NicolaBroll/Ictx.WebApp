﻿using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Ictx.WebApp.Infrastructure.Data;
using Ictx.WebApp.Core.Models;

namespace Ictx.WebApp.Infrastructure.Repositories
{
    public class GenericRepository<T> where T : class
    {
        internal AppDbContext context;
        internal DbSet<T> dbSet;

        public GenericRepository(AppDbContext context)
        {
            this.context = context;
            this.dbSet = context.Set<T>();
        }

        public async virtual Task<bool> AnyAsync(Expression<Func<T, bool>> filter = null)
        {
            IQueryable<T> query = dbSet;

            if(filter != null)
                return await query.AnyAsync(filter);

            return await query.AnyAsync();
        }

        public virtual IQueryable<T> QueryMany(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = "")
        {
            IQueryable<T> query = dbSet;

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

        public async virtual Task<PageResult<T>> ReadManyPaginatedAsync(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = "",
            PaginationModel paginationModel = null)
        { 
            if (paginationModel == null)
                throw new ArgumentNullException();

            IQueryable<T> qy = QueryMany(filter, orderBy, includeProperties);

            var count = qy.Count();
            var list = await qy.Skip((paginationModel.Page - 1) * paginationModel.PageSize).Take(paginationModel.PageSize).ToListAsync();

            return new PageResult<T>(list, count);
        }

        public async virtual Task<T> ReadAsync(object id)
        {
            return await dbSet.FindAsync(id);
        }

        public async virtual Task InsertAsync(T entity)
        {
            await dbSet.AddAsync(entity);
        }
        public async virtual Task InsertManyAsync(IEnumerable<T> entity)
        {
            await dbSet.AddRangeAsync(entity);
        }

        public virtual void Delete(object id)
        {
            T entityToDelete = dbSet.Find(id);
            Delete(entityToDelete);
        }
        public virtual void Delete(T entityToDelete)
        {
            if (context.Entry(entityToDelete).State == EntityState.Detached)
                dbSet.Attach(entityToDelete);

            dbSet.Remove(entityToDelete);
        }

        public virtual void Update(T entityToUpdate)
        {
            dbSet.Attach(entityToUpdate);
            context.Entry(entityToUpdate).State = EntityState.Modified;
        }
    }
}
