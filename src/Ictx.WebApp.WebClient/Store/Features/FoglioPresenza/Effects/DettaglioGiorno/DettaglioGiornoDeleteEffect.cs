using System;
using System.Net.Http;
using System.Threading.Tasks;
using Fluxor;
using Ictx.WebApp.WebClient.Services;
using Ictx.WebApp.WebClient.Store.Features.FoglioPresenza.Actions.DettaglioGiorno;
using Microsoft.Extensions.Logging;

namespace Ictx.WebApp.WebClient.Store.Features.FoglioPresenza.Effects.DettaglioGiorno
{
   public class DettaglioGiornoDeleteEffect : Effect<DeleteDettaglioGiornoAction>
    {
        private readonly ILogger<DettaglioGiornoDeleteEffect> _logger;
        private readonly FoglioPresenzaDettaglioGiornoService _foglioPresenzaDettaglioGiornoService;

        public DettaglioGiornoDeleteEffect(ILogger<DettaglioGiornoDeleteEffect> logger, FoglioPresenzaDettaglioGiornoService foglioPresenzaDettaglioGiornoService)
        {
            this._logger = logger;
            this._foglioPresenzaDettaglioGiornoService = foglioPresenzaDettaglioGiornoService;
        }

        protected override async Task HandleAsync(DeleteDettaglioGiornoAction action, IDispatcher dispatcher)
        {
            try
            {
                _logger.LogInformation($"Deleting todo {action.Id}...");
                var deleteResponse = await _foglioPresenzaDettaglioGiornoService.Delete(action.Id);

                if (!deleteResponse)
                {
                    throw new HttpRequestException($"Error deleting dettaglio giorno {action.Id}");
                }

                _logger.LogInformation($"Todo deleted successfully!");
                dispatcher.Dispatch(new DeleteDettaglioGiornoSuccessAction(action.Id));
            }
            catch (Exception e)
            {
                _logger.LogError($"Could not create todo, reason: {e.Message}");
                dispatcher.Dispatch(new DeleteDettaglioGiornoFailureAction(e.Message));
            }
        }
    }
} 
