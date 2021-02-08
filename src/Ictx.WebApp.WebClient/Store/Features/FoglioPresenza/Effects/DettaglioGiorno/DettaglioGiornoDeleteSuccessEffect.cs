using System.Threading.Tasks;
using Fluxor;
using Ictx.WebApp.WebClient.Store.Features.FoglioPresenza.Actions.DettaglioGiorno;
using Microsoft.Extensions.Logging;

namespace Ictx.WebApp.WebClient.Store.Features.FoglioPresenza.Effects.DettaglioGiorno
{
    public class DettaglioGiornoDeleteSuccessEffect : Effect<DeleteDettaglioGiornoSuccessAction>
    {
        private readonly ILogger<DettaglioGiornoDeleteEffect> _logger;

        public DettaglioGiornoDeleteSuccessEffect(ILogger<DettaglioGiornoDeleteEffect> logger)
        {
            this._logger = logger;
        }

        protected override Task HandleAsync(DeleteDettaglioGiornoSuccessAction action, IDispatcher dispatcher)
        {
            _logger.LogInformation("Deleted todo successfully, navigating back to todo listing...");

            return Task.CompletedTask;
        }
    }
} 
