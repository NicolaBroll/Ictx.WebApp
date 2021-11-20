using System;
using System.Collections.Generic;
using Ictx.WebApp.Core.Exceptions;

namespace Ictx.WebApp.Core.Models;

public class OperationResult<T>
{
    public bool IsSuccess { get; }
    public bool IsFail { get; }

    public T ResultData { get; }
    public Exception Exception { get; }

    private OperationResult(T data)
    {
        this.IsSuccess = true;
        this.ResultData = data;
    }

    private OperationResult(Exception ex)
    {
        this.IsFail = true;
        this.Exception = ex;
    }

    // Success.
    public static OperationResult<T> Success(T data) =>  new OperationResult<T>(data);    


    // Not found.
    internal static OperationResult<T> NotFound() => new OperationResult<T>(new NotFoundException());

    internal static OperationResult<T> NotFound(string message) => new OperationResult<T>(new NotFoundException(message));


    // Invalid.
    internal static OperationResult<T> Invalid(Dictionary<string, IEnumerable<string>> dictionaryErrors) => new OperationResult<T>(new BadRequestException(dictionaryErrors));
}
