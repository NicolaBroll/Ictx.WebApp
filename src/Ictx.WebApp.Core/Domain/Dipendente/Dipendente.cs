﻿using System;
using Ictx.WebApp.Core.Entities.Base;

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
