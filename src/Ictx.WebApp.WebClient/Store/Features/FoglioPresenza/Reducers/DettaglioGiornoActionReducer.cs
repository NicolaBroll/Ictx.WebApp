using System.Linq;
using Fluxor;
using Ictx.WebApp.WebClient.Store.Features.FoglioPresenza.Actions.DettaglioGiorno;
using Ictx.WebApp.WebClient.Store.State;

namespace Ictx.WebApp.WebClient.Store.Features.FoglioPresenza.Reducers
{
    public static class DettaglioGiornoActionReducer
    {
        [ReducerMethod]
        public static FoglioPresenzaState ReduceDeleteDettaglioGiornoAction(FoglioPresenzaState state, DeleteDettaglioGiornoAction _)
        {
            return new FoglioPresenzaState(true, null, state.FoglioPresenza);
        }

        [ReducerMethod]
        public static FoglioPresenzaState ReduceDeleteTodoSuccessAction(FoglioPresenzaState state, DeleteDettaglioGiornoSuccessAction action)
        {
            if (state.FoglioPresenza == null)            
                return new FoglioPresenzaState(false, null, null);            

            var update = state.FoglioPresenza;

            // Rimuovo il dettaglio giorno.
            foreach(var giorno in update.Giorni)            
                giorno.Dettagli = giorno.Dettagli.Where(x => x.Id != action.Id).ToList();            

            return new FoglioPresenzaState(false, null, update);
        }

        [ReducerMethod]
        public static FoglioPresenzaState ReduceDeleteTodoFailureAction(FoglioPresenzaState state, DeleteDettaglioGiornoFailureAction action)
        {
            return new FoglioPresenzaState(false, action.ErrorMessage, state.FoglioPresenza);
        }
    }
}
