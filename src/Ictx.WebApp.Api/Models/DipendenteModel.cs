using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Ictx.WebApp.Core.Models;
using Newtonsoft.Json;

namespace Ictx.WebApp.Api.Models
{
    public class DipendenteQueryParameters : PaginationModel
    {
        public int DittaId { get; set; }
    }

    public class DipendenteDto
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("dittaId")]
        [DisplayName("Identificativo ditta")]
        [Required(ErrorMessage = "Il campo {0} è obbligatorio.")]
        public int DittaId { get; set; }

        [JsonProperty("codiceFiscale")]
        [DisplayName("Codice fiscale")]
        [Required(ErrorMessage = "Il campo {0} è obbligatorio.")]
        [StringLength(16, MinimumLength = 16, ErrorMessage = "Il campo {0} dev'essere di 16 caratteri. ")]
        public string CodiceFiscale { get; set; }

        [JsonProperty("cognome")]
        [Required(ErrorMessage = "Il campo {0} è obbligatorio.")]
        [StringLength(64, ErrorMessage = "Il campo {0} non può superare i {1} caratteri. ")]
        public string Cognome { get; set; }

        [JsonProperty("nome")]
        [Required(ErrorMessage = "Il campo {0} è obbligatorio.")]
        [StringLength(64, ErrorMessage = "Il campo {0} non può superare i {1} caratteri. ")]
        public string Nome { get; set; }

        [JsonProperty("sesso")]
        [Required(ErrorMessage = "Il campo {0} è obbligatorio.")]
        [EnumDataType(typeof(Sesso), ErrorMessage = "Il valore inserito non è valido per il campo {0}.")]
        public string Sesso { get; set; }

        [JsonProperty("dataNascita")]
        [DisplayName("Data nascita")]
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
