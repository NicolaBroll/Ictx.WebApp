using System.Linq;
using System.Collections.Generic;
using FluentValidation;
using Ictx.WebApp.Application.Models;
using Ictx.WebApp.Application.Contracts.UnitOfWork;

namespace Ictx.WebApp.Application.BO;

public class BaseBO<T>
{
    protected readonly IAppUnitOfWork _appUnitOfWork;
    protected readonly IValidator<T>  _validator;

    public BaseBO(IAppUnitOfWork appUnitOfWork, IValidator<T> validator)
    {
        this._appUnitOfWork = appUnitOfWork;
        this._validator     = validator;
    }

    protected OperationResult<T> Validation(T value)
    {
        var validationResult = this._validator.Validate(value);

        if (validationResult.IsValid)
        {
            return OperationResult<T>.Success(value);
        }

        var dictionaryErrors = validationResult.Errors
            .GroupBy(x => x.PropertyName)
            .ToDictionary(k => k.Key, v => v.Select(x => x.ErrorMessage));

        return OperationResult<T>.Invalid(dictionaryErrors);
    }

    protected OperationResult<List<T>> Validation(List<T> list)
    {
        foreach (var item in list)
        {
            var res = this._validator.Validate(item);

            if (!res.IsValid)
            {
                var dictionaryErrors = res.Errors
                    .GroupBy(x => x.PropertyName)
                    .ToDictionary(k => k.Key, v => v.Select(x => x.ErrorMessage));

                return OperationResult<List<T>>.Invalid(dictionaryErrors);
            }
        }

        return OperationResult<List<T>>.Success(list);
    }
}

