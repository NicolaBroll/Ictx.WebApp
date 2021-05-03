using System;

namespace Ictx.WebApp.Core.Models
{
    public class OperationResult<T>
    {
        public bool IsSuccess { get; }
        public bool IsFail { get { return !IsSuccess; } }

        public T ResultData { get; set; }
        public Exception Exception { get; set; }

        public OperationResult(T data)
        {
            this.IsSuccess = true;
            this.ResultData = data;
        }

        public OperationResult(Exception ex)
        {
            this.IsSuccess = false;
            this.Exception = ex;
        }
    }
}