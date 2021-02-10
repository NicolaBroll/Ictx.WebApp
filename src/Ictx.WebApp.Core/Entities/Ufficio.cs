using System;
using System.Collections.Generic;
using Ictx.WebApp.Core.Base;

namespace Ictx.WebApp.Core.Entities
{
    public class Ufficio : BaseEntity
    {
        public int UfficioBaseId { get; set; } 
        public int CodiceUfficio { get; set; }
        public string Denominazione { get; set; }
        public DateTime Inserted { get; set; }
        public DateTime Updated { get; set; }


        // Relazioni.
        public UfficioBase UfficioBase { get; set; }
        public List<Impresa> LstImprese { get; set; }

        public override string ToString()
        {
            return base.ToString() + $" Ufficio: {CodiceUfficio} Denominazione:{Denominazione}";
        }

        public Ufficio()
        {
            
        }

        public Ufficio(int codiceUfficio, string denominazione, UfficioBase ufficioBase)
        {   
            var dateNow = DateTime.UtcNow;

            this.CodiceUfficio = codiceUfficio;
            this.Denominazione = denominazione;
            this.UfficioBase = ufficioBase;

            this.Inserted = dateNow;
            this.Updated = dateNow;
        }
    }

}
