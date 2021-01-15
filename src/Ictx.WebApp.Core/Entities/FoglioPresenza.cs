using System;
using System.Collections.Generic;
using Ictx.WebApp.Core.Base;

namespace Ictx.WebApp.Core.Entities
{
    public class FoglioPresenza : BaseEntity
    {
        public int DipendenteId { get; set; }
        public int Anno { get; set; }
        public int Mese { get; set; }
        public DateTime Inserted { get; set; }
        public DateTime Updated { get; set; }

        // Relazioni
        public Dipendente Dipendente { get; set; }
        public List<FoglioPresenzaGiorno> Giorni { get; set; }

        public FoglioPresenza()
        { }

        public FoglioPresenza(int idDipendente, int anno, int mese)
        {
            DipendenteId = idDipendente;
            Anno = anno;
            Mese = mese;

            var time = DateTime.UtcNow;

            this.Inserted = time;
            this.Updated = time;

            this.Giorni = new List<FoglioPresenzaGiorno>();
        }

        public override string ToString()
        {
            return base.ToString() + $" DipendenteId:{DipendenteId} Anno:{Anno} Mese:{Mese}";
        }
    }
}
