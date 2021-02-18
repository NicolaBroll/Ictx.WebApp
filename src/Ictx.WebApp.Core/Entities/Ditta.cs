using System;
using System.Collections.Generic;
using Ictx.WebApp.Core.Base;

namespace Ictx.WebApp.Core.Entities
{
    public class Ditta : BaseEntity
    {
        public int ImpresaId { get; set; }
        public int CodiceDitta { get; set; }
        public string Denominazione { get; set; }
        public DateTime Inserted { get; set; }
        public DateTime Updated { get; set; }


        // Relazioni.
        public Impresa Impresa { get; set; }
        public List<Dipendente> LstDipendenti { get; set; }


        public Ditta()
        {
            
        }

        public Ditta(int codiceDitta, string denominazione, Impresa impresa)
        {
            var dateNow = DateTime.UtcNow;

            this.CodiceDitta = codiceDitta;
            this.Denominazione = denominazione;
            this.Impresa = impresa;

            this.Inserted = dateNow;
            this.Updated = dateNow;  
        }

        public override string ToString()
        {
            return base.ToString() + $"CodiceDitta:{CodiceDitta} Denominazione:{Denominazione}";
        }
    }

}
