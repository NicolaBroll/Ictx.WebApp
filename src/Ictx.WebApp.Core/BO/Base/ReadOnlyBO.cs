using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using Ictx.WebApp.Core.Models;
using Ictx.WebApp.Core.Contracts.UnitOfWork;

namespace Ictx.WebApp.Core.BO.Base;

public abstract class ReadOnlyBO<T, K, Q> where Q : PaginationModel
{
    protected readonly IAppUnitOfWork _appUnitOfWork;

    public ReadOnlyBO(IAppUnitOfWork appUnitOfWork)
    {
        this._appUnitOfWork = appUnitOfWork;
    }

    // Read many paginated.
    public async Task<PageResult<T>> ReadManyPaginatedAsync(Q filter, CancellationToken cancellationToken = default)
    {
        return await ReadManyPaginatedViewsAsync(filter, cancellationToken);
    }

    protected virtual async Task<PageResult<T>> ReadManyPaginatedViewsAsync(Q filter, CancellationToken cancellationToken)
    {
        return await Task.FromException<PageResult<T>>(new NotImplementedException());
    }

    // Read many.
    public async Task<IEnumerable<T>> ReadManydAsync(Q filter, CancellationToken cancellationToken = default)
    {
        return await ReadManyViewsAsync(filter, cancellationToken);
    }  
    
    protected virtual async Task<IEnumerable<T>> ReadManyViewsAsync(Q filter, CancellationToken cancellationToken)
    {
        return await Task.FromException<List<T>>(new NotImplementedException());
    }

    // Read.
    public async Task<OperationResult<T>> ReadAsync(K key, CancellationToken cancellationToken = default)
    { 
        return await ReadViewAsync(key, cancellationToken);
    }

    protected virtual async Task<OperationResult<T>> ReadViewAsync(K key, CancellationToken cancellationToken)
    {
        return await Task.FromException<OperationResult<T>>(new NotImplementedException());
    }   
}