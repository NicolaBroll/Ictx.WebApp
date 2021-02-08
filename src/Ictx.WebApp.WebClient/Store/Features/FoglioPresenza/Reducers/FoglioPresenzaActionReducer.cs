using Fluxor;
using Ictx.WebApp.WebClient.Store.Features.FoglioPresenza.Actions.Load;
using Ictx.WebApp.WebClient.Store.State;

namespace Ictx.WebApp.WebClient.Store.Features.FoglioPresenza.Reducers
{
    public static class FoglioPresenzaActionsReducer
    {
        [ReducerMethod]
        public static FoglioPresenzaState ReduceLoadFoglioPresenzaAction(FoglioPresenzaState state, LoadFoglioPresenzaAction _) =>
            new FoglioPresenzaState(true, null, null);

        [ReducerMethod]
        public static FoglioPresenzaState ReduceLoadFoglioPresenzaSuccessAction(FoglioPresenzaState state, LoadFoglioPresenzaSuccessAction action) =>
            new FoglioPresenzaState(false, null, action.FoglioPresenza);

        [ReducerMethod]
        public static FoglioPresenzaState ReduceLoadFoglioPresenzaFailureAction(FoglioPresenzaState state, LoadFoglioPresenzaFailureAction action) =>
            new FoglioPresenzaState(false, action.ErrorMessage, null);
    }
}
