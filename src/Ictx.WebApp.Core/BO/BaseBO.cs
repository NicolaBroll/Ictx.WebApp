using System;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using FluentValidation;
using Ictx.WebApp.Core.Models;
using Ictx.WebApp.Core.Contracts.UnitOfWork;

namespace Ictx.WebApp.Core.BO;

public abstract class BaseBO<T, K, Q> where Q : PaginationModel
{
    protected readonly IAppUnitOfWork _appUnitOfWork;
    protected readonly IValidator<T>  _validator;

    public BaseBO(IAppUnitOfWork appUnitOfWork, IValidator<T> validator)
    {
        this._appUnitOfWork = appUnitOfWork;
        this._validator     = validator;
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
    protected virtual async Task<IEnumerable<T>> ReadManyViewsAsync(Q filter, CancellationToken cancellationToken)
    {
        return await Task.FromException<List<T>>(new NotImplementedException());
    }

    public async Task<IEnumerable<T>> ReadManydAsync(Q filter, CancellationToken cancellationToken = default)
    {
        return await ReadManyViewsAsync(filter, cancellationToken);
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


    // Delete.
    public async Task<OperationResult<bool>> DeleteAsync(K key, CancellationToken cancellationToken = default)
    {
        return await DeleteViewAsync(key, cancellationToken);
    }

    protected virtual async Task<OperationResult<bool>> DeleteViewAsync(K key, CancellationToken cancellationToken)
    {
        return await Task.FromException<OperationResult<bool>>(new NotImplementedException());
    }


    // Save.
    public async Task<OperationResult<T>> SaveAsync(K key, T value, CancellationToken cancellationToken = default)
    {
        var validazione = await ValidationSingleAsync(value);

        if (validazione.IsFail)
        {
            return validazione;
        }

        return await SaveViewAsync(key, value, cancellationToken);       
    }

    protected virtual async Task<OperationResult<T>> SaveViewAsync(K key, T value, CancellationToken cancellationToken)
    {
        return await Task.FromException<OperationResult<T>>(new NotImplementedException());
    }


    // Insert.
    public async Task<OperationResult<T>> InsertAsync(T value, CancellationToken cancellationToken = default)
    {
        var validazione = await ValidationSingleAsync(value);

        if (validazione.IsFail)
        {
            return validazione;
        }

        return await InsertViewAsync(value, cancellationToken);
    }

    protected virtual async Task<OperationResult<T>> InsertViewAsync(T value, CancellationToken cancellationToken)
    {
        return await Task.FromException<OperationResult<T>>(new NotImplementedException());
    }


    // Insert many.
    public async Task<OperationResult<List<T>>> InsertManyAsync(List<T> value, CancellationToken cancellationToken = default)
    {
        var validazione = await ValidationManyAsync(value);

        if (validazione.IsFail)
        {
            return validazione;
        }

        return await InsertManyViewsAsync(value, cancellationToken);
    }

    protected virtual async Task<OperationResult<List<T>>> InsertManyViewsAsync(List<T> value, CancellationToken cancellationToken)
    {
        return await Task.FromException<OperationResult<List<T>>>(new NotImplementedException());
    }


    // Validation single.
    public async Task<OperationResult<T>> ValidationSingleAsync(T value)
    {
        return await ValidationSingleViewAsync(value);
    }

    protected virtual async Task<OperationResult<T>> ValidationSingleViewAsync(T value)
    {
        if (this._validator is null)
        {
            return OperationResult<T>.Success(value);
        }

        var validationResult = await this._validator.ValidateAsync(value);

        if (validationResult.IsValid)
        {
            return OperationResult<T>.Success(value);
        }

        var dictionaryErrors = FromValidationFailureToDictionary(validationResult);

        return OperationResult<T>.Invalid(dictionaryErrors);
    }


    // Validation multiple.
    public async Task<OperationResult<List<T>>> ValidationManyAsync(List<T> value)
    {
        return await ValidationManyViewAsync(value);
    }

    protected virtual async Task<OperationResult<List<T>>> ValidationManyViewAsync(List<T> list)
    {
        if (this._validator is null)
        {
            return OperationResult<List<T>>.Success(list);
        }

        foreach (var item in list)
        {
            var validationResult = await this._validator.ValidateAsync(item);

            if (!validationResult.IsValid)
            {
                var dictionaryErrors = FromValidationFailureToDictionary(validationResult);
                return OperationResult<List<T>>.Invalid(dictionaryErrors);
            }
        }

        return OperationResult<List<T>>.Success(list);
    }

    private static Dictionary<string, IEnumerable<string>> FromValidationFailureToDictionary(FluentValidation.Results.ValidationResult res)
    {
        return res.Errors
            .GroupBy(x => x.PropertyName)
            .ToDictionary(k => k.Key, v => v.Select(x => x.ErrorMessage));
    }
}