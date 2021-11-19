using System;

namespace Ictx.WebApp.Application.Models;

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

    public static OperationResult<T> Success(T data) =>  new OperationResult<T>(data);    

    public static OperationResult<T> Fail(Exception ex) => new OperationResult<T>(ex);    
}
