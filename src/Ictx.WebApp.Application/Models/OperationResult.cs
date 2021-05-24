using System;

namespace Ictx.WebApp.Application.Models
{
    public class OperationResult<T>
    {
        public bool IsSuccess { get; }
        public bool IsFail { get; } 

        public T ResultData { get; }
        public Exception Exception { get; }

        public OperationResult(T data)
        {
            this.IsSuccess = true;
            this.ResultData = data;
        }

        public OperationResult(Exception ex)
        {
            this.IsFail = true;
            this.Exception = ex;
        }
    }
}