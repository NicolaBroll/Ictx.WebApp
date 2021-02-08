using Ictx.WebApp.Shared.Dtos;

namespace Ictx.WebApp.WebClient.Store.Features.FoglioPresenza.Actions.Load
{
    public class LoadFoglioPresenzaSuccessAction
    {
        public LoadFoglioPresenzaSuccessAction(FoglioPresenzaDto foglioPresenza)
        {
            this.FoglioPresenza = foglioPresenza;
        }

        public FoglioPresenzaDto FoglioPresenza { get; }
    }
}
