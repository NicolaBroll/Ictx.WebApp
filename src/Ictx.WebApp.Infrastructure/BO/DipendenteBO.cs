using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Ictx.Framework.Models;
using Ictx.Framework.BO;
using Ictx.WebApp.Core.Entities;
using Ictx.WebApp.Infrastructure.Models;
using Ictx.WebApp.Infrastructure.UnitOfWork;
using Ictx.WebApp.Core.Exceptions.Dipendente;
using Ictx.WebApp.Infrastructure.Services.Interfaces;
using Ictx.WebApp.Infrastructure.BO.Interfaces;

namespace Ictx.WebApp.Infrastructure.BO
{
    public class DipendenteBO: BaseBO<Dipendente, int, DipendenteListFilter>, IDipendenteBO
    {
        private readonly IAppUnitOfWork     _appUnitOfWork;
        private readonly IDateTimeService   _dateTimeService;
        private readonly IDittaBO           _dittaBO;

        public DipendenteBO(IAppUnitOfWork appUnitOfWork, IDateTimeService dateTimeService, IDittaBO dittaBO)
        {
            this._appUnitOfWork     = appUnitOfWork;
            this._dateTimeService   = dateTimeService;
            this._dittaBO           = dittaBO;
        }

        /// <summary>
        /// Ritorna una lista di dipendenti paginata per una determinata ditta.
        /// </summary>
        /// <param name="filter">Id della ditta e parametri di paginazione</param>
        /// <returns>Ritorna unoggetto contenente la lista di dipendenti paginata e il totalcount dei record su DB</returns>
        protected override async Task<PageResult<Dipendente>> ReadManyViewsAsync(DipendenteListFilter filter)
        {
            if (filter.DittaId <= 0)
            {
                return new PageResult<Dipendente>(new List<Dipendente>(), 0);
            }

            var result = await this._appUnitOfWork.DipendenteRepository.ReadManyPaginatedAsync(
                filter: x => x.DittaId == filter.DittaId,
                orderBy: x => x.OrderBy(o => o.Cognome).ThenBy(x => x.Nome),
                paginationModel: filter);

            return result;
        }

        /// <summary>
        /// Ritorna un dipendente. Se non viene trovato, ritorna DipendenteNotFoundException.
        /// </summary>
        /// <param name="key">Id dipendente</param>
        /// <returns>Ritorna un Result<Dipendente> contenente il dipendente associato all'id richiesto oppure una 
        /// DipendenteNotFoundException nel caso il dipendente non sia presente. </returns>
        protected override async Task<BOResult<Dipendente>> ReadViewAsync(int key)
        {
            var dipendente = await this._appUnitOfWork.DipendenteRepository.ReadAsync(key);

            if (dipendente is null)
            {
                return new BOResult<Dipendente>(new NotFoundException($"Dipendente con id: {key} non trovato."));
            }

            return new BOResult<Dipendente>(dipendente);
        }

        /// <summary>
        /// Crea un dipendente.
        /// </summary>
        /// <param name="value">Modello contenente i dati del nuovo dipendente.</param>
        /// <returns>Ritorna un Result<Dipendente> contenente il dipendente creato.
        /// Se il dipendente non viene trovato, ritorna DipendenteNotFoundException.
        /// Se la ditta non viene trovata, ritorna DittaNotFoundException.
        /// </returns>
        protected override async Task<BOResult<Dipendente>> InsertViewAsync(Dipendente value)
        {
            // Leggo la ditta.
            var dittaResult = await this._dittaBO.ReadAsync(value.DittaId);

            if (dittaResult.IsFail)
            {
                return new BOResult<Dipendente>(new BadRequestException(dittaResult.Exception.Message));
            }

            var utcNow = this._dateTimeService.UtcNow;

            var objToInsert = new Dipendente
            {
                DittaId = dittaResult.ResultData.Id,
                CodiceFiscale = value.CodiceFiscale.ToUpper(),
                Cognome = value.Cognome.ToUpper(),
                Nome = value.Nome.ToUpper(),
                Sesso = value.Sesso,
                DataNascita = value.DataNascita,
                Inserted = utcNow,
                Updated = utcNow,
            };

            await this._appUnitOfWork.DipendenteRepository.InsertAsync(objToInsert);
            await this._appUnitOfWork.SaveAsync();

            return new BOResult<Dipendente>(objToInsert);
        }

        /// <summary>
        /// Modifica un dipendente.
        /// </summary>
        /// <param name="key">Id dipendente da modificare.</param>
        /// <param name="value">Modello contenente i nuovi dati.</param>
        /// <returns>Ritorna un Result<Dipendente> contenente il dipendente modificato.
        /// Se il dipendente non viene trovato, ritorna DipendenteNotFoundException.
        /// Se la ditta non viene trovata, ritorna DittaNotFoundException.
        /// </returns>
        protected override async Task<BOResult<Dipendente>> SaveViewAsync(int key, Dipendente value)
        {
            var objToUpdate = await this._appUnitOfWork.DipendenteRepository.ReadAsync(key);

            if (objToUpdate is null)
            {
                return new BOResult<Dipendente>(new NotFoundException($"Dipendente con id: {key} non trovato."));
            }

            // Leggo la ditta.
            var dittaResult = await this._dittaBO.ReadAsync(value.DittaId);

            if (dittaResult.IsFail)
            {
                return new BOResult<Dipendente>(new BadRequestException(dittaResult.Exception.Message));
            }

            objToUpdate.CodiceFiscale = value.CodiceFiscale.ToUpper();
            objToUpdate.Nome = value.Nome.ToUpper();
            objToUpdate.Cognome = value.Cognome.ToUpper();
            objToUpdate.Sesso = value.Sesso;
            objToUpdate.DataNascita = value.DataNascita;
            objToUpdate.Updated = this._dateTimeService.UtcNow;
            objToUpdate.DittaId = dittaResult.ResultData.Id;

            this._appUnitOfWork.DipendenteRepository.Update(objToUpdate);
            await this._appUnitOfWork.SaveAsync();

            return new BOResult<Dipendente>(objToUpdate);
        }

        /// <summary>
        /// Elimina un dipendente. Se non viene trovato, ritorna DipendenteNotFoundException.
        /// </summary>
        /// <param name="id">Id dipendente</param>
        /// <returns>Ritorna un Result<Dipendente> contenente il dipendente eliminato. Oppure una 
        /// DipendenteNotFoundException nel caso il dipendente non sia presente. </returns>
        protected override async Task<BOResult<bool>> DeleteViewAsync(int key)
        {
            var objToDelete = await this._appUnitOfWork.DipendenteRepository.ReadAsync(key);

            if (objToDelete is null)
            {
                return new BOResult<bool>(new NotFoundException($"Dipendente con id: {key} non trovato."));
            }

            this._appUnitOfWork.DipendenteRepository.Delete(objToDelete);
            await this._appUnitOfWork.SaveAsync();

            return new BOResult<bool>(true);
        }
    }
}
