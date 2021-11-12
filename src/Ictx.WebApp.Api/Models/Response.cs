using System.Linq;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Ictx.WebApp.Api.Models;

public class ErrorResponseDto
{
    [JsonPropertyName("message")]
    public string Message { get; set; }

    [JsonPropertyName("errors")]
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
    [JsonPropertyName("data")]
    public IEnumerable<T> Data { get; set; }

    [JsonPropertyName("totalCount")]
    public int TotalCount { get; set; }
}