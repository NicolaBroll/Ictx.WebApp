using System;

namespace Ictx.WebApp.Core.Exceptions.Dipendente
{
    public class BadRequestException : Exception
    {
        public BadRequestException(string message = "") : base(String.IsNullOrEmpty(message) ? "I dati inseriti non sono corretti." : message)
        { }
    }
}
