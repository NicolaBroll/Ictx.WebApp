using Newtonsoft.Json;
using System.Collections.Generic;

namespace Ictx.WebApp.Api.Models
{
    public class ErrorResponse
    {
        public string Title { get; private set; }
        public string Message { get; private set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public Dictionary<string, IEnumerable<string>> Errors { get; private set; }

        public ErrorResponse(string title, string message)
        {
            this.Title = title;
            this.Message = message;
        }

        public ErrorResponse(Dictionary<string, IEnumerable<string>> errors)
        {
            this.Title = "Errore durante la validazione dei campi.";
            this.Message = "Verificare i dati e riprovare.";
            this.Errors = errors;
        }
    }
}
