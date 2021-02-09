using System.ComponentModel.DataAnnotations;

namespace Ictx.WebApp.Shared.QueryParameters
{
    public class FoglioPresenzaQueryParameter
    {
        public int DipendenteId { get; set; }
        public int Anno { get; set; }
        public int Mese { get; set; }

        public override string ToString()
        {
            return base.ToString() + $" DipendenteId:{DipendenteId} Anno:{Anno} Mese:{Mese}";
        }
    }
}
