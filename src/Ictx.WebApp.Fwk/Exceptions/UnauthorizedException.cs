using System;

namespace Ictx.WebApp.Fwk.Exceptions
{
    public class NotFoundException: Exception
    {
        public NotFoundException(string message = "") 
            : base(String.IsNullOrEmpty(message) ? "Dato non trovato." : message)
        { }
    }
}
