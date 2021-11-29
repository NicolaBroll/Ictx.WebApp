using Ictx.WebApp.Core.Models;
using System.ComponentModel;
using System.Text.Json.Serialization;

namespace Ictx.WebApp.Core.Domain.Dipendente;

public enum Sesso
{
    [Description("Maschio")]
    M,
    [Description("Femmina")]
    F
}

public class DipendenteFilter : PaginationModel
{
    [JsonPropertyName("id")]
    public int? Id { get; set; }

    [JsonPropertyName("idDitta")]
    public int? IdDitta { get; set; }
}

