using System;

namespace Ictx.WebApp.Core.Exceptions.Dipendente
{
    public class DipendenteNotFoundException : Exception
    {
        public DipendenteNotFoundException(int id) : base($"Dipendente con id: {id} non trovato.")
        {
        }
    }
}
