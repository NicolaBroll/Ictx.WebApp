using System.ComponentModel;

namespace Ictx.WebApp.Core.Models
{
    public static class DipendenteModel
    {
        public enum Sesso
        {
            [Description("Maschio")]
            M,
            [Description("Femmina")]
            F
        }
    }
}
