using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using FluentValidation;
using FluentValidation.Results;
using Ictx.WebApp.Fwk.Models;
using Microsoft.EntityFrameworkCore;
using Ictx.WebApp.Fwk.Exceptions;

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

        if (validazione.Exception is not null)
        {
            return validazione;
        }

        return await SaveViewAsync(key, value, cancellationToken);       
    }

    protected virtual async Task<(TEntity Data, Exception Exception)> SaveViewAsync(TKey key, TEntity value, CancellationToken cancellationToken)
    {
        return await Task.FromException<(TEntity Data, Exception Exception)>(new NotImplementedException());
    }

    #endregion

    #region Insert

    public async Task<(TEntity Data, Exception Exception)> InsertAsync(TEntity value, CancellationToken cancellationToken = default)
    {
        var validazione = await ValidationSingleAsync(value);

        if (validazione.Exception is not null)
        {
            return validazione;
        }

        return await InsertViewAsync(value, cancellationToken);
    }

    protected virtual async Task<(TEntity Data, Exception Exception)> InsertViewAsync(TEntity value, CancellationToken cancellationToken)
    {
        return await Task.FromException<(TEntity Data, Exception Exception)>(new NotImplementedException());
    }

    #endregion

    #region Insert many

    public async Task<(List<TEntity> Data, Exception Exception)> InsertManyAsync(List<TEntity> value, CancellationToken cancellationToken = default)
    {
        var validazione = await ValidationManyAsync(value);

        if (validazione.Exception is not null)
        {
            return validazione;
        }

        return await InsertManyViewsAsync(value, cancellationToken);
    }

    protected virtual async Task<(List<TEntity> Data, Exception Exception)> InsertManyViewsAsync(List<TEntity> value, CancellationToken cancellationToken)
    {
        return await Task.FromException<(List<TEntity> Data, Exception Exception)>(new NotImplementedException());
    }

    #endregion

    #region Validation 
    
    // Single.
    public async Task<(TEntity Data, Exception Exception)> ValidationSingleAsync(TEntity value)
    {
        return await ValidationSingleViewAsync(value);
    }

    protected virtual async Task<(TEntity Data, Exception Exception)> ValidationSingleViewAsync(TEntity value)
    {
        if (this._validator is null)
        {
            return (value, null);
        }

        var validationResult = await this._validator.ValidateAsync(value);

        if (validationResult.IsValid)
        {
            return (value, null);
        }

        var dictionaryErrors = FromValidationFailureToDictionary(validationResult);

        return (default(TEntity), new BadRequestException(dictionaryErrors));
    }


    // Multiple.
    public async Task<(List<TEntity> Data, Exception Exception)> ValidationManyAsync(List<TEntity> value)
    {
        return await ValidationManyViewAsync(value);
    }

    protected virtual async Task<(List<TEntity> Data, Exception Exception)> ValidationManyViewAsync(List<TEntity> list)
    {
        if (this._validator is null)
        {
            return (list, null);
        }

        foreach (var item in list)
        {
            var validationResult = await this._validator.ValidateAsync(item);

            if (!validationResult.IsValid)
            {
                var dictionaryErrors = FromValidationFailureToDictionary(validationResult);
                return (null, new BadRequestException(dictionaryErrors));
            }
        }

        return (list, null);
    }

    private static Dictionary<string, IEnumerable<string>> FromValidationFailureToDictionary(ValidationResult res) => 
        res.Errors
            .GroupBy(x => x.PropertyName)
            .ToDictionary(k => k.Key, v => v.Select(x => x.ErrorMessage));

    #endregion

}