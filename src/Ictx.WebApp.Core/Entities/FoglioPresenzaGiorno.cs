using Ictx.WebApp.Core.Base;
using System;
using System.Collections.Generic;

namespace Ictx.WebApp.Core.Entities
{
    public class FoglioPresenzaGiorno : BaseEntity
    {
        public int FoglioPresenzaId { get; set; }
        public int Giorno { get; set; }
        public int Tipo { get; set; }
        public bool Quadratura { get; set; }
        public DateTime Inserted { get; set; }
        public DateTime Updated { get; set; }

        // Relazioni.
        public FoglioPresenza FoglioPresenza { get; set; }
        public List<FoglioPresenzaGiornoDettaglio> Dettagli { get; set; }

        public FoglioPresenzaGiorno()
        { }

        public FoglioPresenzaGiorno(int giorno, int tipo, bool quadratura)
        {
            this.Giorno = giorno;
            this.Tipo = tipo;
            this.Quadratura = quadratura;

            var time = DateTime.UtcNow;

            this.Inserted = time;
            this.Updated = time;

            this.Dettagli = new List<FoglioPresenzaGiornoDettaglio>();
        }

        public override string ToString()
        {
            return base.ToString() + $" Giorno:{Giorno} Tipo:{Tipo} Quadratura:{Quadratura}";
        }
    }
}
