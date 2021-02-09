using System.Collections.Generic;

namespace Ictx.WebApp.Api.Dtos
{
    public static class FoglioPresenzaDtos
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
        
        public class FoglioPresenzaGiornoDettaglioDto
        {
            public int Id { get; set; }
            public int Ore { get; set; }
            public int Minuti { get; set; }
            public int Tipo { get; set; }

            public FoglioPresenzaVpaDto Vpa { get; set; }

            public override string ToString()
            {
                return base.ToString() + $" Ore:{Ore} Minuti:{Minuti} Tipo:{Tipo}";
            }
        }

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

        public class FoglioPresenzaVpaDto
        {
            public string Sigla { get; set; }

            public override string ToString()
            {
                return base.ToString() + $" Sigla:{Sigla}";
            }
        }
    }
}