using Fluxor;
using Microsoft.Extensions.Logging;
using Ictx.WebApp.WebClient.Store.Features.FoglioPresenza.Actions;

namespace Ictx.WebApp.WebClient.Services
{
    public class StateFacade
    {
        private readonly ILogger<StateFacade> _logger;
        private readonly IDispatcher _dispatcher;

        public StateFacade(ILogger<StateFacade> logger, IDispatcher dispatcher) =>
            (_logger, _dispatcher) = (logger, dispatcher);

        public void LoadFoglioPresenza(int dipendenteId, int anno, int mese)
        {
            _logger.LogInformation("Issuing action to load foglio presenza...");
            _dispatcher.Dispatch(new LoadFoglioPresenzaAction(dipendenteId, anno, mese));
        }
    }
}