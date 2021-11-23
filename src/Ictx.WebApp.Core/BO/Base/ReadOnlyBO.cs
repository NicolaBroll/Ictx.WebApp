using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using Ictx.WebApp.Core.Models;
using Ictx.WebApp.Core.Contracts.UnitOfWork;

namespace Ictx.WebApp.Core.BO.Base;

public abstract class ReadOnlyBO<TEntity, TKey, TParameters> where TParameters : PaginationModel
{
    protected readonly IAppUnitOfWork _appUnitOfWork;

    public ReadOnlyBO(IAppUnitOfWork appUnitOfWork)
    {
        this._appUnitOfWork = appUnitOfWork;
    }

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

    #region Query many

    public IQueryable<TEntity> QueryManyAsync(TParameters filter, CancellationToken cancellationToken = default)
    {
        return QueryManyViewsAsync(filter, cancellationToken);
    }

    protected virtual IQueryable<TEntity> QueryManyViewsAsync(TParameters filter, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    #endregion

    #region Read

    public async Task<OperationResult<TEntity>> ReadAsync(TKey key, CancellationToken cancellationToken = default)
    { 
        return await ReadViewAsync(key, cancellationToken);
    }

    protected virtual async Task<OperationResult<TEntity>> ReadViewAsync(TKey key, CancellationToken cancellationToken)
    {
        return await Task.FromException<OperationResult<TEntity>>(new NotImplementedException());
    }

    #endregion

}