using System;
using System.ComponentModel.DataAnnotations;
using static Ictx.WebApp.Core.Models.DipendenteModel;

namespace Ictx.WebApp.Api.Dtos
{
    public static class DipendenteDtos
    {
        public class DipendenteDto
        {
            public int Id { get; set; }

            [Required(ErrorMessage = "Il campo {0} è obbligatorio.")]
            public int DittaId { get; set; }

            [Required(ErrorMessage = "Il campo {0} è obbligatorio.")]
            [StringLength(16, MinimumLength = 16, ErrorMessage = "Il campo {0} dev'essere di 16 caratteri. ")]
            public string CodiceFiscale { get; set; }

            [Required(ErrorMessage = "Il campo {0} è obbligatorio.")]
            [StringLength(64, ErrorMessage = "Il campo {0} non può superare i {1} caratteri. ")]
            public string Cognome { get; set; }

            [Required(ErrorMessage = "Il campo {0} è obbligatorio.")]
            [StringLength(64, ErrorMessage = "Il campo {0} non può superare i {1} caratteri. ")]
            public string Nome { get; set; }

            [Required(ErrorMessage = "Il campo {0} è obbligatorio.")]
            [EnumDataType(typeof(Sesso), ErrorMessage = "Il valore inserito non è valido per il campo {0}.")]
            public string Sesso { get; set; }

            [Required(ErrorMessage = "Il campo {0} è obbligatorio.")]
            public DateTime DataNascita { get; set; }

            public DipendenteDto()
            { }

            public DipendenteDto(int dittaId, string codiceFiscale, string cognome, string nome, string sesso, DateTime dataNascita)
            {
                this.DittaId = dittaId;
                this.CodiceFiscale = codiceFiscale;
                this.Cognome = cognome;
                this.Nome = nome;
                this.Sesso = sesso;
                this.DataNascita = dataNascita;
            }
        }
    }
}
