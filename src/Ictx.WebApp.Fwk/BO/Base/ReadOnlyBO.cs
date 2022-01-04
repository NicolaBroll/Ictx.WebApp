using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using Ictx.WebApp.Fwk.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Ictx.WebApp.Fwk.BO.Base;

public abstract class ReadOnlyBO<TEntity, TKey, TParameters> where TParameters : PaginationModel
{
    #region Read many

    public async Task<IEnumerable<TEntity>> ReadManyAsync(TParameters filter, CancellationToken cancellationToken = default)
    {
        return await ReadManyViewsAsync(filter, cancellationToken);
    }

    protected virtual async Task<IEnumerable<TEntity>> ReadManyViewsAsync(TParameters filter, CancellationToken cancellationToken)
    {
        return await Task.FromException<List<TEntity>>(new NotImplementedException());
    }

    // Paginated.
    public async Task<PageResult<TEntity>> ReadManyPaginatedAsync(TParameters filter, CancellationToken cancellationToken = default)
    {
        return await ReadManyPaginatedViewsAsync(filter, cancellationToken);
    }

    protected virtual async Task<PageResult<TEntity>> ReadManyPaginatedViewsAsync(TParameters filter, CancellationToken cancellationToken)
    {
        return await Task.FromException<PageResult<TEntity>>(new NotImplementedException());
    }

    #endregion

    #region Read

    public async Task<(TEntity Data, Exception Exception)> ReadAsync(TKey key, CancellationToken cancellationToken = default)
    { 
        return await ReadViewAsync(key, cancellationToken);
    }

    protected virtual async Task<(TEntity data, Exception exception)> ReadViewAsync(TKey key, CancellationToken cancellationToken)
    {
        return await Task.FromException<(TEntity, Exception)> (new NotImplementedException());
    }

    #endregion

    public async virtual Task<PageResult<TData>> GetPaginatedResult<TData>(IQueryable<TData> query, PaginationModel pagination, CancellationToken cancellationToken = default)
    {
        if (pagination is null)
        {
            throw new ArgumentException("PaginationModel is null.");
        }

        // Pagination.
        var count = query.Count();
        var list = await query.Skip((pagination.Page - 1) * pagination.PageSize).Take(pagination.PageSize).ToListAsync(cancellationToken: cancellationToken);

        return new PageResult<TData>(list, count);
    }
}