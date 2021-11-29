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

    public class DipendenteFilter : PaginationModel
    {
        public int? Id { get; set; }
        public int? IdDitta { get; set; }
    }
}
