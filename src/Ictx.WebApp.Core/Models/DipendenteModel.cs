﻿using System.ComponentModel;

namespace Ictx.WebApp.Core.Models
{
    public enum Sesso
    {
        [Description("Maschio")]
        M,
        [Description("Femmina")]
        F
    }

    public class DipendenteListFilter : PaginationModel 
    {
        public int DittaId { get; set; }
    }    
}
