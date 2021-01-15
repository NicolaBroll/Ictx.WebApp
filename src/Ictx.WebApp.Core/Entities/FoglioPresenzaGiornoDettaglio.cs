using Ictx.WebApp.Core.Base;
using System;

namespace Ictx.WebApp.Core.Entities
{
    public class FoglioPresenzaGiornoDettaglio : BaseEntity
    {
        public int FoglioPresenzaGiornoId { get; set; }
        public int FoglioPresenzaVpaId { get; set; }
        public int Ore { get; set; }
        public int Minuti { get; set; }
        public int Tipo { get; set; }
        public DateTime Inserted { get; set; }
        public DateTime Updated { get; set; }

        // Relazioni.
        public FoglioPresenzaGiorno Giorno { get; set; }
        public FoglioPresenzaVpa Vpa { get; set; }

        // Costruttori.
        public FoglioPresenzaGiornoDettaglio()
        { }

        public FoglioPresenzaGiornoDettaglio(int ore, int minuti, int tipo, FoglioPresenzaVpa vpa)
        {
            this.Ore = ore;
            this.Minuti = minuti;
            this.Tipo = tipo;
            this.Vpa = vpa;

            var time = DateTime.UtcNow;

            this.Inserted = time;
            this.Updated = time;
        }

        // Metodi.
        public override string ToString()
        {
            return base.ToString() + $" Ore:{Ore} Minuti:{Minuti} Tipo:{Tipo}";
        }
    }
}
