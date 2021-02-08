namespace Ictx.WebApp.WebClient.Store.Features.FoglioPresenza.Actions.Load
{
    public class LoadFoglioPresenzaAction
    {
        public int DipendenteId { get; }
        public int Anno { get; }
        public int Mese { get; }

        public LoadFoglioPresenzaAction(int dipendenteId, int anno, int mese)
        {
            DipendenteId = dipendenteId;
            Anno = anno;
            Mese = mese;
        }
    }
}
