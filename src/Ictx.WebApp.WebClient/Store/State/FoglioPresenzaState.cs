using Ictx.WebApp.Shared.Dtos;

namespace Ictx.WebApp.WebClient.Store.State
{
    public class FoglioPresenzaState : RootState
    {
        public FoglioPresenzaState(bool isLoading, string currentErrorMessage, FoglioPresenzaDto foglioPresenza) 
            : base(isLoading, currentErrorMessage)
        {
            this.FoglioPresenza = foglioPresenza;
        }

        public FoglioPresenzaDto FoglioPresenza { get; }
    }
}