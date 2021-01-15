using System;
using System.Threading.Tasks;
using Ictx.WebApp.Core.Entities;
using Ictx.WebApp.Infrastructure.Services.Interface;
using Ictx.WebApp.Infrastructure.UnitOfWork;
using Microsoft.Extensions.Logging;
using static Ictx.WebApp.Core.Models.FoglioPresenzaModel;

namespace Ictx.WebApp.Infrastructure.Services.Implementation
{
    public class FoglioPresenzaService : IFoglioPresenzaService
    {
        private readonly ILogger<FoglioPresenzaService> _logger;
        private readonly AppUnitOfWork _appUnitOfWork;

        public FoglioPresenzaService(ILogger<FoglioPresenzaService> logger, AppUnitOfWork appUnitOfWork)
        {
            this._logger = logger;
            this._appUnitOfWork = appUnitOfWork;
        }

        public async Task<FoglioPresenza> Get(int dipendenteId, int anno, int mese)
        {
            var fdp = await this._appUnitOfWork.FoglioPresenzaRepository.GetFoglioPresenza(dipendenteId, anno, mese);

            if (fdp == null)
            {
                // Se il foglio presenza non esiste lo creo.
                fdp = await CreaFoglioPresenza(dipendenteId, anno, mese);
            }

            return fdp;
        }

        private async Task<FoglioPresenza> CreaFoglioPresenza(int dipendenteId, int anno, int mese)
        {
            var fdp = new FoglioPresenza(dipendenteId, anno, mese);

            var daysInMonth = DateTime.DaysInMonth(anno, mese);
            var vpaOR = await this._appUnitOfWork.FoglioPresenzaVpaRepository.GetBySigla("OR");

            for(var i = 1; i<= daysInMonth; i++)
            {
                var giorno = new FoglioPresenzaGiorno(i, (int)FoglioPresenzaGiornoTipo.Tipo0, true);
                var dettaglioGiorno = new FoglioPresenzaGiornoDettaglio(8, 0, (int) FoglioPresenzaGiornoDettaglioTipo.Tipo0, vpaOR);

                giorno.Dettagli.Add(dettaglioGiorno);
                fdp.Giorni.Add(giorno);
            }

            await this._appUnitOfWork.FoglioPresenzaRepository.InsertAsync(fdp);
            await this._appUnitOfWork.Save();

            return fdp;
        }
    }
}
