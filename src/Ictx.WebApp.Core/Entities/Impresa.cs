using System;
using System.Collections.Generic;
using Ictx.WebApp.Core.Entities.Base;

namespace Ictx.WebApp.Core.Entities
{
    public class Impresa : BaseEntity
    {
        public int UfficioId { get; set; }
        public int CodiceImpresa { get; set; }
        public string Denominazione { get; set; }
        public DateTime Inserted { get; set; }
        public DateTime Updated { get; set; }


        // Relazioni.
        public Ufficio Ufficio { get; set; }
        public List<Ditta> LstDitte { get; set; }

        public Impresa()
        {
        }

        public Impresa(int codiceImpresa, string denominazione, Ufficio ufficio)
        {
            var dateNow = DateTime.UtcNow;

            this.CodiceImpresa = codiceImpresa;
            this.Denominazione = denominazione;
            this.Ufficio = ufficio;

            this.Inserted = dateNow;
            this.Updated = dateNow;
        }

        public override string ToString()
        {
            return base.ToString() + $"CodiceImpresa:{CodiceImpresa} Denominazione:{Denominazione}";
        }
    }

}
