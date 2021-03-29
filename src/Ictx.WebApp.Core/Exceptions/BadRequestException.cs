using System;

namespace Ictx.WebApp.Core.Exceptions.Dipendente
{
    public class BadRequestException : Exception
    {
        public BadRequestException(int id) : base($"Dipendente con id: {id} non trovato.")
        {
        }
    }
}
