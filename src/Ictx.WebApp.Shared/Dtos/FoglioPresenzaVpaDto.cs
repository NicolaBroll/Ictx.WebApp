namespace Ictx.WebApp.Shared.Dtos
{
    public class FoglioPresenzaVpaDto
    {
        public string Sigla { get; set; }
        public int Codice { get; set; }
        public string Descrizione { get; set; }

        public override string ToString()
        {
            return base.ToString() + $" Sigla:{Sigla} Codice:{Codice} Descrizione:{Descrizione}";
        }
    }
}
