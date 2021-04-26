using System;
using System.Collections.Generic;
using Ictx.WebApp.Core.Entities.Base;

namespace Ictx.WebApp.Core.Entities
{
    public class UfficioBase : BaseEntity
    {
        public string Denominazione { get; set; }
        public int CodiceUfficioBase { get; set; }
        public DateTime Inserted { get; set; }
        public DateTime Updated { get; set; }

        // Relazioni.
        public List<Ufficio> LstUffici { get; set; }


        public UfficioBase()
        {
            
        }

        public UfficioBase(int codiceUfficioBase, string denominazione)
        {   
            var dateNow = DateTime.UtcNow;

            this.CodiceUfficioBase = codiceUfficioBase;
            this.Denominazione = denominazione;

            this.Inserted = dateNow;
            this.Updated = dateNow;
        }

        public override string ToString()
        {
            return base.ToString() + $"CodiceUfficioBase:{CodiceUfficioBase} Denominazione:{Denominazione}";
        }
    }

}
