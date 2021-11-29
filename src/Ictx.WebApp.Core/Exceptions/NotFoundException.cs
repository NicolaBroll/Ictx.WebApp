using System;

namespace Ictx.WebApp.Core.Exceptions
{
    public class UnauthorizedException : Exception
    {
        public UnauthorizedException(string message = "") 
            : base(String.IsNullOrEmpty(message) ? "Accesso alla risorsa non autorizzato." : message)
        { }
    }
}
