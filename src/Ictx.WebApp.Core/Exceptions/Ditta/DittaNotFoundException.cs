using System;

namespace Ictx.WebApp.Core.Exceptions.Dipendente
{
    public class DittaNotFoundException : Exception
    {
        public DittaNotFoundException(int id) : base($"Ditta con id: {id} non trovata.")
        {
        }
    }
}
