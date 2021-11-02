using System;
using Ictx.WebApp.Core.Entities.Base;

namespace Ictx.WebApp.Core.Entities
{
    public class Dipendente : BaseEntity
    {
        public string CodiceFiscale { get; set; }
        public string Cognome { get; set; }
        public string Nome { get; set; }
        public string Sesso { get; set; }
        public DateTime DataNascita { get; set; }
        public bool IsDeleted { get; set; }

        public override string ToString()
        {
            return base.ToString() + $" CodiceFiscale:{CodiceFiscale} Cognome:{Cognome} Nome:{Nome}";
        }

        public Dipendente()
        { }

        public Dipendente(string codiceFiscale, string cognome, string nome, string sesso, DateTime dataNascita)
        {
            this.CodiceFiscale = codiceFiscale;
            this.Cognome = cognome;
            this.Nome = nome;
            this.Sesso = sesso;
            this.DataNascita = dataNascita;
        }
    }
}
