using System;
using System.ComponentModel;
using System.Text.Json.Serialization;
using Ictx.WebApp.Fwk.Entities.Base;
using Ictx.WebApp.Fwk.Models;

namespace Ictx.WebApp.Core.Domain.Dipendente;

public class Dipendente : BaseEntity<int>
{
    public int IdDitta { get; set; }
    public string Cognome { get; set; }
    public string Nome { get; set; }
    public Sesso Sesso { get; set; }

    /// <summary>
    /// Espresso in local time.
    /// </summary>
    public DateTime DataNascita { get; set; }

    public override string ToString()
    {
        return base.ToString() + $" IdDitta:{IdDitta} Cognome:{Cognome} Nome:{Nome}";
    }

    public Dipendente()
    { }

    public Dipendente(int idDitta, string cognome, string nome, Sesso sesso, DateTime dataNascita)
    {
        this.IdDitta = idDitta;
        this.Cognome = cognome;
        this.Nome = nome;
        this.Sesso = sesso;
        this.DataNascita = dataNascita;
    }
}

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

