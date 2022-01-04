using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using FluentValidation;
using FluentValidation.Results;
using Ictx.WebApp.Fwk.Models;
using Microsoft.EntityFrameworkCore;

namespace Ictx.WebApp.Fwk.BO.Base;

public abstract class PersistableBO<TEntity, TKey, TParameters> : ReadOnlyBO<TEntity, TKey, TParameters> where TParameters : PaginationModel
{
    protected readonly IValidator<TEntity> _validator;

    public PersistableBO(IValidator<TEntity> validator = null)
    {
        this._validator     = validator;
    }

    #region Delete

    public async Task<(bool Data, Exception Exception)> DeleteAsync(TKey key, CancellationToken cancellationToken = default)
    {
        return await DeleteViewAsync(key, cancellationToken);
    }

    protected virtual async Task<(bool Data, Exception Exception)> DeleteViewAsync(TKey key, CancellationToken cancellationToken)
    {
        return await Task.FromException<(bool Data, Exception Exception)>(new NotImplementedException());
    }

    #endregion

    #region Save

    public async Task<(TEntity Data, Exception Exception)> SaveAsync(TKey key, TEntity value, CancellationToken cancellationToken = default)
    {
        var validazione = await ValidationSingleAsync(value);

        if (validazione.IsFail)
        {
            return validazione;
        }

        return await SaveViewAsync(key, value, cancellationToken);       
    }

    protected virtual async Task<OperationResult<TEntity>> SaveViewAsync(TKey key, TEntity value, CancellationToken cancellationToken)
    {
        return await Task.FromException<OperationResult<TEntity>>(new NotImplementedException());
    }

    #endregion

    #region Insert

    public async Task<OperationResult<TEntity>> InsertAsync(TEntity value, CancellationToken cancellationToken = default)
    {
        var validazione = await ValidationSingleAsync(value);

        if (validazione.IsFail)
        {
            return validazione;
        }

        return await InsertViewAsync(value, cancellationToken);
    }

    protected virtual async Task<OperationResult<TEntity>> InsertViewAsync(TEntity value, CancellationToken cancellationToken)
    {
        return await Task.FromException<OperationResult<TEntity>>(new NotImplementedException());
    }

    #endregion

    #region Insert many

    public async Task<OperationResult<List<TEntity>>> InsertManyAsync(List<TEntity> value, CancellationToken cancellationToken = default)
    {
        var validazione = await ValidationManyAsync(value);

        if (validazione.IsFail)
        {
            return validazione;
        }

        return await InsertManyViewsAsync(value, cancellationToken);
    }

    protected virtual async Task<OperationResult<List<TEntity>>> InsertManyViewsAsync(List<TEntity> value, CancellationToken cancellationToken)
    {
        return await Task.FromException<OperationResult<List<TEntity>>>(new NotImplementedException());
    }

    #endregion

    #region Validation 
    
    // Single.
    public async Task<OperationResult<TEntity>> ValidationSingleAsync(TEntity value)
    {
        return await ValidationSingleViewAsync(value);
    }

    protected virtual async Task<OperationResult<TEntity>> ValidationSingleViewAsync(TEntity value)
    {
        if (this._validator is null)
        {
            return OperationResult<TEntity>.Success(value);
        }

        var validationResult = await this._validator.ValidateAsync(value);

        if (validationResult.IsValid)
        {
            return OperationResult<TEntity>.Success(value);
        }

        var dictionaryErrors = FromValidationFailureToDictionary(validationResult);

        return OperationResult<TEntity>.Invalid(dictionaryErrors);
    }


    // Multiple.
    public async Task<OperationResult<List<TEntity>>> ValidationManyAsync(List<TEntity> value)
    {
        return await ValidationManyViewAsync(value);
    }

    protected virtual async Task<OperationResult<List<TEntity>>> ValidationManyViewAsync(List<TEntity> list)
    {
        if (this._validator is null)
        {
            return OperationResult<List<TEntity>>.Success(list);
        }

        foreach (var item in list)
        {
            var validationResult = await this._validator.ValidateAsync(item);

            if (!validationResult.IsValid)
            {
                var dictionaryErrors = FromValidationFailureToDictionary(validationResult);
                return OperationResult<List<TEntity>>.Invalid(dictionaryErrors);
            }
        }

        return OperationResult<List<TEntity>>.Success(list);
    }

    private static Dictionary<string, IEnumerable<string>> FromValidationFailureToDictionary(ValidationResult res) => 
        res.Errors
            .GroupBy(x => x.PropertyName)
            .ToDictionary(k => k.Key, v => v.Select(x => x.ErrorMessage));

    #endregion

}