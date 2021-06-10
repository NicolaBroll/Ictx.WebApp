using System;

namespace Ictx.WebApp.Core.Models
{
    public class Utente
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Cognome { get; set; }
        public string Email { get; set; }
    }
}
