using System.Linq;
using System.Collections.Generic;
using FluentValidation;
using Ictx.WebApp.Core.Models;
using Ictx.WebApp.Core.Contracts.UnitOfWork;

namespace Ictx.WebApp.Core.BO;

public class BaseBO<T>
{
    protected readonly IAppUnitOfWork _appUnitOfWork;
    protected readonly IValidator<T>  _validator;

    public BaseBO(IAppUnitOfWork appUnitOfWork, IValidator<T> validator)
    {
        this._appUnitOfWork = appUnitOfWork;
        this._validator     = validator;
    }

    #region Validation

    protected OperationResult<T> Validation(T value)
    {
        var validationResult = this._validator.Validate(value);

        if (validationResult.IsValid)
        {
            return OperationResult<T>.Success(value);
        }

        var dictionaryErrors = FromValidationFailureToDictionary(validationResult);

        return OperationResult<T>.Invalid(dictionaryErrors);
    }

    protected OperationResult<List<T>> Validation(List<T> list)
    {
        foreach (var item in list)
        {
            var validationResult = this._validator.Validate(item);

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
    
    #endregion
}