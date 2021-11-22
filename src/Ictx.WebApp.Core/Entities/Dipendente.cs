using System;
using Ictx.WebApp.Core.Entities.Base;
using Ictx.WebApp.Core.Models;

namespace Ictx.WebApp.Core.Entities
{
    public class Dipendente : BaseEntity<int>
    {
        public string Cognome { get; set; }
        public string Nome { get; set; }
        public Sesso Sesso { get; set; }

        /// <summary>
        /// Espresso in local time.
        /// </summary>
        public DateTime DataNascita { get; set; }

        public override string ToString()
        {
            return base.ToString() + $" Cognome:{Cognome} Nome:{Nome}";
        }

        public Dipendente()
        { }

        public Dipendente(string cognome, string nome, Sesso sesso, DateTime dataNascita)
        {
            this.Cognome = cognome;
            this.Nome = nome;
            this.Sesso = sesso;
            this.DataNascita = dataNascita;
        }
    }
}
