namespace Ictx.WebApp.WebClient.Store.Features.FoglioPresenza.Actions
{
    public abstract class FailureAction
    {
        protected FailureAction(string errorMessage) => 
            ErrorMessage = errorMessage;

        public string ErrorMessage { get; }
    }
}