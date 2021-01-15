using System.Collections.Generic;

namespace Ictx.WebApp.Shared.Dtos
{
    public class FoglioPresenzaDto
    {
        public int DipendenteId { get; set; }
        public int Anno { get; set; }
        public int Mese { get; set; }

        public List<FoglioPresenzaGiornoDto> Giorni { get; set; }

        public override string ToString()
        {
            return base.ToString() + $" DipendenteId:{DipendenteId} Anno:{Anno} Mese:{Mese}";
        }
    }
}
