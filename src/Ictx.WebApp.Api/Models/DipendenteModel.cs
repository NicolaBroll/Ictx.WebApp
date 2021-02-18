using static Ictx.WebApp.Core.Models.PaginationModel;

namespace Ictx.WebApp.Api.Models
{
    public static class DipendenteModel
    {
        public class DipendenteQueryParameters : PaginationFilterModel
        {
            public int DittaId { get; set; }
        }
    }
}
