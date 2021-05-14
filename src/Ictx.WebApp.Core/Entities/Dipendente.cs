using System;
using Ictx.WebApp.Core.Entities.Base;
using Ictx.WebApp.Core.Models;

namespace Ictx.WebApp.Core.Entities
{
    public class Dipendente : BaseEntity
    {
        private string _codiceFiscale;
        private string _cognome;
        private string _nome;

        public string CodiceFiscale
        {
            get => this._codiceFiscale;
            set => this._codiceFiscale = value.ToUpper();
        }

        public string Cognome
        {
            get => this._cognome;
            set => this._cognome = value.ToUpper();
        }

        public string Nome
        {
            get => this._nome;
            set => this._nome = value.ToUpper();
        }

        public Sesso Sesso { get; set; }
        public DateTime DataNascita { get; set; }
        public bool IsDeleted { get; set; }

        public override string ToString()
        {
            return base.ToString() + $" CodiceFiscale:{CodiceFiscale} Cognome:{Cognome} Nome:{Nome}";
        }

        public Dipendente()
        { }

        public Dipendente(string codiceFiscale, string cognome, string nome, Sesso sesso, DateTime dataNascita)
        {
            this.CodiceFiscale = codiceFiscale;
            this.Cognome = cognome;
            this.Nome = nome;
            this.Sesso = sesso;
            this.DataNascita = dataNascita;
        }
    }

}
