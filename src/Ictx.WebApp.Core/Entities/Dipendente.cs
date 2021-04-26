using System;
using Ictx.WebApp.Core.Entities.Base;
using Ictx.WebApp.Core.Models;

namespace Ictx.WebApp.Core.Entities
{
    public class Dipendente : BaseEntity
    {
        public int DittaId { get; set; }
        public string CodiceFiscale { get; set; }
        public string Cognome { get; set; }
        public string Nome { get; set; }
        public Sesso Sesso { get; set; }
        public DateTime DataNascita { get; set; }
        public DateTime Inserted { get; set; }
        public DateTime Updated { get; set; }

        // Relazioni.
        public Ditta Ditta { get; set; }

        public override string ToString()
        {
            return base.ToString() + $" CodiceFiscale:{CodiceFiscale} Cognome:{Cognome} Nome:{Nome}";
        }

        public Dipendente()
        { }

        public Dipendente(int dittaId, string codiceFiscale, string cognome, string nome, Sesso sesso, DateTime dataNascita)
        {
            this.DittaId = dittaId;
            this.CodiceFiscale = codiceFiscale;
            this.Cognome = cognome;
            this.Nome = nome;
            this.Sesso = sesso;
            this.DataNascita = dataNascita;
        }
    }

}
