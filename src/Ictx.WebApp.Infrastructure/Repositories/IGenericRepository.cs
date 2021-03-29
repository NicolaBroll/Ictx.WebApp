using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Ictx.WebApp.Infrastructure.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        Task<bool> AnyAsync();
        void Delete(object id);
        void Delete(T entityToDelete);
        Task InsertAsync(T entity);
        Task InsertManyAsync(IEnumerable<T> entity);
        IQueryable<T> QueryMany(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties = "");
        Task<T> ReadAsync(object id);
        Task<IEnumerable<T>> ReadManyAsync(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties = "");
        void Update(T entityToUpdate);
    }
}