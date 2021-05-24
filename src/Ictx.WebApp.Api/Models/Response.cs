using System.Linq;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Ictx.WebApp.Api.Models
{
    public class ErrorResponseDto
    {
        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "errors")]
        public Dictionary<string, IEnumerable<string>> Errors { get; set; }

        public ErrorResponseDto()
        { }

        public ErrorResponseDto(string message)
        {
            this.Message = message;
        }

        public ErrorResponseDto(string message, Dictionary<string, IEnumerable<string>> errors)
        {
            this.Message = message;

            if (errors.Any()) 
            {
                this.Errors = errors;
            }
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
