using System;

namespace Ictx.WebApp.Infrastructure.BO.Base
{
    public class BOResult<T>
    {
        public bool IsSuccess { get; }
        public bool IsFail { get { return !IsSuccess; } }

        public T ResultData { get; set; }
        public Exception Exception { get; set; }

        public BOResult(T data)
        {
            this.IsSuccess = true;
            this.ResultData = data;
        }

        public BOResult(Exception ex)
        {
            this.IsSuccess = false;
            this.Exception = ex;
        }
    }
}
