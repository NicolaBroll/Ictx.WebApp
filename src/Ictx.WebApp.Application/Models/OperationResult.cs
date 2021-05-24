using System;

namespace Ictx.WebApp.Application.Models
{
    public class OperationResult<T>
    {
        public bool IsSuccess { get => ResultData != null; }
        public bool IsFail { get => Exception != null; } 

        public T ResultData { get; }
        public Exception Exception { get; }

        public OperationResult(T data)
        {
            this.ResultData = data;
        }

        public OperationResult(Exception ex)
        {
            this.Exception = ex;
        }
    }
}