using Newtonsoft.Json;
using System.Collections.Generic;

namespace Ictx.WebApp.Api.Models
{
    public class ErrorResponseDto
    {
        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "errors")]
        public Dictionary<string, IEnumerable<string>> Errors { get; set; }

        public ErrorResponseDto()
        { }

        public ErrorResponseDto(string title, string message)
        {
            this.Title = title;
            this.Message = message;
        }

        public ErrorResponseDto(Dictionary<string, IEnumerable<string>> errors)
        {
            this.Title = "Errore durante la validazione dei campi.";
            this.Message = "Verificare i dati e riprovare.";
            this.Errors = errors;
        }
    }

    public class PageResultDto<T>
    {
        [JsonProperty("data")]
        public IEnumerable<T> Data { get; set; }

        [JsonProperty("totalCount")]
        public int TotalCount { get; set; }
    }
}
