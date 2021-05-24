using System.ComponentModel;

namespace Ictx.WebApp.Core.Models
{
    public enum Sesso
    {
        [Description("Maschio")]
        M,
        [Description("Femmina")]
        F
    }

    public class DipendenteEmailTemplate
    {
        public string Nome { get; set; }
        public string Cognome { get; set; }
        public string CodiceFiscale { get; set; }
    }
}
