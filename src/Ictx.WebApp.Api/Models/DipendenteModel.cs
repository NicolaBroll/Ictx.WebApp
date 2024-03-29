﻿using System;
using System.Text.Json.Serialization;

namespace Ictx.WebApp.Api.Models;

public class DipendenteDto
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("cognome")]    
    public string Cognome { get; set; }

    [JsonPropertyName("nome")]       
    public string Nome { get; set; }

    [JsonPropertyName("sesso")]      
    public string Sesso { get; set; }

    [JsonPropertyName("dataNascita")]        
    public DateTime DataNascita { get; set; }

    public DipendenteDto()
    { }

    public DipendenteDto(string cognome, string nome, string sesso, DateTime dataNascita)
    {
        this.Cognome = cognome;
        this.Nome = nome;
        this.Sesso = sesso;
        this.DataNascita = dataNascita;
    }
}