using System;

namespace Ictx.WebApp.Core.Exceptions.Dipendente
{
    public class NotFoundException: Exception
    {
        public NotFoundException(string message = "") : base(
            String.IsNullOrEmpty(message) ? "I dato non trovato." : message
        )
        { }
    }
}
