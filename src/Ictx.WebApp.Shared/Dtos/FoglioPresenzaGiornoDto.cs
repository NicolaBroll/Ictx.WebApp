using System.Collections.Generic;

namespace Ictx.WebApp.Shared.Dtos
{
    public class FoglioPresenzaGiornoDto
    {
        public int Giorno { get; set; }
        public int Tipo { get; set; }
        public bool Quadratura { get; set; }

        public List<FoglioPresenzaGiornoDettaglioDto> Dettagli { get; set; }

        public override string ToString()
        {
            return base.ToString() + $" Giorno:{Giorno} Tipo:{Tipo} Quadratura:{Quadratura}";
        }
    }
}
