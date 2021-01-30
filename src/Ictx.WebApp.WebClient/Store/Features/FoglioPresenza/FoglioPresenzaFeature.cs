using Fluxor;
using Ictx.WebApp.WebClient.Store.State;

namespace Ictx.WebApp.WebClient.Store.Features.FoglioPresenza
{
    public class FoglioPresenzaFeature : Feature<FoglioPresenzaState>
    {
        public override string GetName() => "Foglio presenza";

        protected override FoglioPresenzaState GetInitialState() =>
            new FoglioPresenzaState(false, null, null);
    }
}