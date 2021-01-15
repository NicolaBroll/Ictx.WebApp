using System;
using System.ComponentModel.DataAnnotations;

namespace Ictx.WebApp.Shared.Dtos
{
    public class DipendenteDto
    {
        [Required]
        [StringLength(16, MinimumLength = 16, ErrorMessage = "Il {0} dev'essere di 16 caratteri. ")]
        public string CodiceFiscale { get; set; }
        [Required]
        [StringLength(64, ErrorMessage = "Il {0} non può superare i {1} caratteri. ")]
        public string Cognome { get; set; }
        [Required]
        [StringLength(64, ErrorMessage = "Il {0} non può superare i {1} caratteri. ")]
        public string Nome { get; set; }
        [Required]
        public string Sesso { get; set; }
        [Required]
        public DateTime DataNascita { get; set; }
    }
}
