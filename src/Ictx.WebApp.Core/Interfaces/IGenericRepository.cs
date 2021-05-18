using Ictx.WebApp.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Ictx.WebApp.Core.Interfaces
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
        Task<PageResult<T>> ReadManyPaginatedAsync(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties = "", PaginationModel pagination = null);
        void Update(T entityToUpdate);
    }
}
