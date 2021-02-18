using System;
using Ictx.WebApp.Core.Base;

namespace Ictx.WebApp.Core.Entities
{
    public class Comune : BaseEntity
    {
        public string Codice { get; set; }
        public string Provincia { get; set; }
        public string Denominazione { get; set; }
        public DateTime Inserted { get; set; }
        public DateTime Updated { get; set; }

        public Comune()
        {

        }

        public Comune(string codice, string provincia, string denominazione)
        {
            var dateNow = DateTime.UtcNow;

            this.Codice = codice;
            this.Provincia = provincia;
            this.Denominazione = denominazione;

            this.Inserted = dateNow;
            this.Updated = dateNow;
        }

        public override string ToString()
        {
            return base.ToString() + $"Codice:{Codice} Provincia:{Provincia} Denominazione:{Denominazione}";
        }
    }
}
