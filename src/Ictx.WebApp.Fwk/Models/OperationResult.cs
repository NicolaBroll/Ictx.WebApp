using System;
using System.Collections.Generic;
using Ictx.WebApp.Fwk.Exceptions;

namespace Ictx.WebApp.Fwk.Models;

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
    public static OperationResult<T> NotFound(string message = null) => new OperationResult<T>(new NotFoundException(message));

    // Invalid.
    public static OperationResult<T> Invalid(Dictionary<string, IEnumerable<string>> dictionaryErrors) => new OperationResult<T>(new BadRequestException(dictionaryErrors));

    // Unauthorized.
    public static OperationResult<T> Unauthorized(string message = null) => new OperationResult<T>(new UnauthorizedException(message));
}
