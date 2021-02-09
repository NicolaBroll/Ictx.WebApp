using System;
using System.Collections.Generic;
using Ictx.WebApp.Core.Base;
using static Ictx.WebApp.Core.Models.DipendenteModel;

namespace Ictx.WebApp.Core.Entities
{
    public class Dipendente : BaseEntity
    {
        public string CodiceFiscale { get; set; }
        public string Cognome { get; set; }
        public string Nome { get; set; }
        public Sesso Sesso { get; set; }
        public DateTime DataNascita { get; set; }
        public DateTime Inserted { get; set; }
        public DateTime Updated { get; set; }

        public Dipendente() 
        {
        }

        public Dipendente(string codiceFiscale, string cognome, string nome, Sesso sesso, DateTime dataNascita)
        {
            var dateNow = DateTime.UtcNow;

            this.CodiceFiscale = codiceFiscale;
            this.Cognome = Char.ToUpperInvariant(cognome[0]) + cognome.ToLower().Substring(1);
            this.Nome = Char.ToUpperInvariant(nome[0]) + nome.ToLower().Substring(1);
            this.Sesso = sesso;
            this.DataNascita = dataNascita;

            this.Inserted = dateNow;
            this.Updated = dateNow;
        }

        public override string ToString()
        {
            return base.ToString() + $" CodiceFiscale:{CodiceFiscale} Cognome:{Cognome} Nome:{Nome}";
        }
    }

}
