using System.Linq;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Ictx.WebApp.Api.Models;

public class PageResultDto<T>
{
    [JsonPropertyName("data")]
    public IEnumerable<T> Data { get; set; }

    [JsonPropertyName("totalCount")]
    public int TotalCount { get; set; }
}