using System;

namespace Ictx.WebApp.Fwk.Exceptions
{
    public class UnauthorizedException : Exception
    {
        public UnauthorizedException(string message = "") 
            : base(String.IsNullOrEmpty(message) ? "Accesso alla risorsa non autorizzato." : message)
        { }
    }
}
