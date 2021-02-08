using System;
using System.Threading.Tasks;
using Fluxor;
using Ictx.WebApp.WebClient.Services;
using Microsoft.Extensions.Logging;
using Ictx.WebApp.WebClient.Store.Features.FoglioPresenza.Actions.Load;

namespace Ictx.WebApp.WebClient.Store.Features.FoglioPresenza.Effects.Load
{
    public class LoadFoglioPresenzaEffect : Effect<LoadFoglioPresenzaAction>
    {
        private readonly ILogger<LoadFoglioPresenzaEffect> _logger;
        private readonly FoglioPresenzaService _foglioPresenzaService;

        public LoadFoglioPresenzaEffect(ILogger<LoadFoglioPresenzaEffect> logger, FoglioPresenzaService foglioPresenzaService)
        {
            this._logger = logger;
            this._foglioPresenzaService = foglioPresenzaService;
        }

        protected override async Task HandleAsync(LoadFoglioPresenzaAction action, IDispatcher dispatcher)
        {
            try
            {
                _logger.LogInformation("Loading FoglioPresenza...");

                // Add a little extra latency for dramatic effect...
                await Task.Delay(TimeSpan.FromMilliseconds(1000));
                var FoglioPresenzaResponse = await this._foglioPresenzaService.Get(action.DipendenteId, action.Anno, action.Mese);

                _logger.LogInformation("FoglioPresenza loaded successfully!");
                dispatcher.Dispatch(new LoadFoglioPresenzaSuccessAction(FoglioPresenzaResponse));
            }
            catch (Exception e)
            {
                _logger.LogError($"Error loading FoglioPresenza, reason: {e.Message}");
                dispatcher.Dispatch(new LoadFoglioPresenzaFailureAction(e.Message));
            }

        }
    }
}
