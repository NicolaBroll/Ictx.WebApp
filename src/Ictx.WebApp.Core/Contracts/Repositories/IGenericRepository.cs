using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System.Collections.Generic;
using Ictx.WebApp.Core.Models;

namespace Ictx.WebApp.Core.Contracts.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        Task<bool> AnyAsync(Expression<Func<T, bool>> filter = null, CancellationToken cancellationToken = default);
        Task Delete(object id, CancellationToken cancellationToken = default);
        void Delete(T entityToDelete);
        Task InsertAsync(T entity, CancellationToken cancellationToken = default);
        Task InsertManyAsync(IEnumerable<T> entity, CancellationToken cancellationToken = default);
        IQueryable<T> QueryMany(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties = "");
        Task<T> ReadAsync(object id, CancellationToken cancellationToken = default);
        Task<IEnumerable<T>> ReadManyAsync(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties = "", CancellationToken cancellationToken = default);
        Task<PageResult<T>> ReadManyPaginatedAsync(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties = "", PaginationModel pagination = null, CancellationToken cancellationToken = default);
        void Update(T entityToUpdate);

    }
}
