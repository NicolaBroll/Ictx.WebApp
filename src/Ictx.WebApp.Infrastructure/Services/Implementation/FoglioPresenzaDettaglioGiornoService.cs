using System.Threading.Tasks;
using Ictx.WebApp.Infrastructure.Services.Interface;
using Ictx.WebApp.Infrastructure.UnitOfWork;
using Microsoft.Extensions.Logging;

namespace Ictx.WebApp.Infrastructure.Services.Implementation
{
    public class FoglioPresenzaDettaglioGiornoService : IFoglioPresenzaDettaglioGiornoService
    {
        private readonly ILogger<FoglioPresenzaService> _logger;
        private readonly AppUnitOfWork _appUnitOfWork;

        public FoglioPresenzaDettaglioGiornoService(ILogger<FoglioPresenzaService> logger, AppUnitOfWork appUnitOfWork)
        {
            this._logger = logger;
            this._appUnitOfWork = appUnitOfWork;
        }

        public async Task Delete(int id)
        {
            this._appUnitOfWork.FoglioPresenzaDettaglioGiornoRepository.Delete(id);
            await this._appUnitOfWork.Save();
        }
    }
}
