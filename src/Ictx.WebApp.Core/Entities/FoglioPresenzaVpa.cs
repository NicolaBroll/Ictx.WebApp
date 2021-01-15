using System;
using System.Collections.Generic;
using Ictx.WebApp.Core.Base;

namespace Ictx.WebApp.Core.Entities
{
    public class FoglioPresenzaVpa : BaseEntity
    {
        public string Sigla { get; set; }
        public int Codice { get; set; }
        public string Descrizione { get; set; }
        public DateTime Inserted { get; set; }
        public DateTime Updated { get; set; }

        // Relazioni.
        public List<FoglioPresenzaGiornoDettaglio> Dettagli { get; set; }

        // Costruttori.
        public FoglioPresenzaVpa()
        { }

        public FoglioPresenzaVpa(string sigla, int codice, string descrizione)
        {
            this.Sigla = sigla;
            this.Codice = codice;
            this.Descrizione = descrizione;

            var time = DateTime.UtcNow;

            this.Inserted = time;
            this.Updated = time;

            this.Dettagli = new List<FoglioPresenzaGiornoDettaglio>();
        }

        // Metodi.
        public override string ToString()
        {
            return base.ToString() + $" Sigla:{Sigla} Codice:{Codice} Descrizione:{Descrizione}";
        }
    }
}
