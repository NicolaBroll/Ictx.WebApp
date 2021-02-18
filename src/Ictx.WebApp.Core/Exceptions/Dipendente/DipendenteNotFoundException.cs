using System;

namespace Ictx.WebApp.Core.Exceptions.Dipendente
{
    public class DipendenteNotFoundException : Exception
    {
        public DipendenteNotFoundException(string message) : base(message)
        {
        }
    }
}
