using System;
using Newtonsoft.Json;

namespace Ictx.WebApp.Api.Models
{
    public class DipendenteDto
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("codiceFiscale")]
        public string CodiceFiscale { get; set; }

        [JsonProperty("cognome")]    
        public string Cognome { get; set; }

        [JsonProperty("nome")]       
        public string Nome { get; set; }

        [JsonProperty("sesso")]      
        public string Sesso { get; set; }

        [JsonProperty("dataNascita")]        
        public DateTime DataNascita { get; set; }

        public DipendenteDto()
        { }

        public DipendenteDto(string codiceFiscale, string cognome, string nome, string sesso, DateTime dataNascita)
        {
            this.CodiceFiscale = codiceFiscale;
            this.Cognome = cognome;
            this.Nome = nome;
            this.Sesso = sesso;
            this.DataNascita = dataNascita;
        }
    }
}
