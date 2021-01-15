namespace Ictx.WebApp.Shared.Dtos
{
    public class FoglioPresenzaGiornoDettaglioDto
    {
        public int Ore { get; set; }
        public int Minuti { get; set; }
        public int Tipo { get; set; }

        public FoglioPresenzaVpaDto Vpa { get; set; }

        public override string ToString()
        {
            return base.ToString() + $" Ore:{Ore} Minuti:{Minuti} Tipo:{Tipo}";
        }
    }
}
