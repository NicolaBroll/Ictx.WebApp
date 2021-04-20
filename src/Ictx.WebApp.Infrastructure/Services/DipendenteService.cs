using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using LanguageExt.Common;
using Ictx.WebApp.Core.Entities;
using Ictx.WebApp.Core.Exceptions.Dipendente;
using Ictx.WebApp.Infrastructure.UnitOfWork;
using Ictx.WebApp.Infrastructure.Services.Interfaces;
using Ictx.Framework.Models;
using Ictx.WebApp.Infrastructure.Models;

namespace Ictx.WebApp.Infrastructure.Services
{
    public class DipendenteService : IDipendenteService
    {
        private readonly IAppUnitOfWork     _appUnitOfWork;
        private readonly IDateTimeService   _dateTimeService;
        private readonly IDittaService      _dittaService;

        public DipendenteService(IAppUnitOfWork appUnitOfWork, IDateTimeService dateTimeService, IDittaService dittaService)
        {
            this._appUnitOfWork     = appUnitOfWork;
            this._dateTimeService   = dateTimeService;
            this._dittaService      = dittaService;
        }

        /// <summary>
        /// Ritorna una lista di dipendenti paginata per una determinata ditta.
        /// </summary>
        /// <param name="filter">Id della ditta e parametri di paginazione</param>
        /// <returns>Ritorna unoggetto contenente la lista di dipendenti paginata e il totalcount dei record su DB</returns>
        public async Task<PageResult<Dipendente>> GetListAsync(DipendenteListFilter filter)
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
        /// <param name="id">Id dipendente</param>
        /// <returns>Ritorna un Result<Dipendente> contenente il dipendente associato all'id richiesto oppure una 
        /// DipendenteNotFoundException nel caso il dipendente non sia presente. </returns>
        public async Task<Result<Dipendente>> GetByIdAsync(int id)
        {
            var dipendente = await this._appUnitOfWork.DipendenteRepository.ReadAsync(id);

            if (dipendente is null)
            {
                return new Result<Dipendente>(new NotFoundException($"Dipendente con id: {id} non trovato."));
            }

            return dipendente;
        }

        /// <summary>
        /// Crea un dipendente.
        /// </summary>
        /// <param name="model">Modello contenente i dati del nuovo dipendente.</param>
        /// <returns>Ritorna un Result<Dipendente> contenente il dipendente creato.
        /// Se il dipendente non viene trovato, ritorna DipendenteNotFoundException.
        /// Se la ditta non viene trovata, ritorna DittaNotFoundException.
        /// </returns>
        public async Task<Result<Dipendente>> InsertAsync(Dipendente model)
        {
            var ditta = await this._appUnitOfWork.DittaRepository.ReadAsync(model.DittaId);

            if (ditta is null)
            {
                return new Result<Dipendente>(new BadRequestException($"Ditta con id: {model.DittaId} non trovata."));
            }

            var utcNow = this._dateTimeService.UtcNow;

            var objToInsert = new Dipendente
            { 
                DittaId = ditta.Id,
                CodiceFiscale = model.CodiceFiscale.ToUpper(),
                Cognome = model.Cognome.ToUpper(),
                Nome = model.Nome.ToUpper(),
                Sesso = model.Sesso,
                DataNascita = model.DataNascita,
                Inserted = utcNow,
                Updated = utcNow,
            };

            await this._appUnitOfWork.DipendenteRepository.InsertAsync(objToInsert);
            await this._appUnitOfWork.SaveAsync();

            return objToInsert;
        }

        /// <summary>
        /// Modifica un dipendente.
        /// </summary>
        /// <param name="id">Id dipendente da modificare.</param>
        /// <param name="model">Modello contenente i nuovi dati.</param>
        /// <returns>Ritorna un Result<Dipendente> contenente il dipendente modificato.
        /// Se il dipendente non viene trovato, ritorna DipendenteNotFoundException.
        /// Se la ditta non viene trovata, ritorna DittaNotFoundException.
        /// </returns>
        public async Task<Result<Dipendente>> SaveAsync(int id, Dipendente model)
        {
            var objToUpdate = await this._appUnitOfWork.DipendenteRepository.ReadAsync(id);

            if (objToUpdate is null)
            {
                return new Result<Dipendente>(new NotFoundException($"Dipendente con id: {id} non trovato."));
            }

            // Leggo la ditta.
            var ditta = await this._appUnitOfWork.DittaRepository.ReadAsync(model.DittaId);

            if (ditta is null)
            {
                return new Result<Dipendente>(new BadRequestException($"Ditta con id: {model.DittaId} non trovata."));
            }

            objToUpdate.CodiceFiscale = model.CodiceFiscale.ToUpper();
            objToUpdate.Nome = model.Nome.ToUpper();
            objToUpdate.Cognome = model.Cognome.ToUpper();
            objToUpdate.Sesso = model.Sesso;
            objToUpdate.DataNascita = model.DataNascita;
            objToUpdate.Updated = this._dateTimeService.UtcNow;
            objToUpdate.DittaId = ditta.Id;

            this._appUnitOfWork.DipendenteRepository.Update(objToUpdate);
            await this._appUnitOfWork.SaveAsync();

            return objToUpdate;
        }

        /// <summary>
        /// Elimina un dipendente. Se non viene trovato, ritorna DipendenteNotFoundException.
        /// </summary>
        /// <param name="id">Id dipendente</param>
        /// <returns>Ritorna un Result<Dipendente> contenente il dipendente eliminato. Oppure una 
        /// DipendenteNotFoundException nel caso il dipendente non sia presente. </returns>
        public async Task<Result<Dipendente>> DeleteAsync(int id)
        {
            var objToDelete = await this._appUnitOfWork.DipendenteRepository.ReadAsync(id);

            if (objToDelete is null)
            {
                return new Result<Dipendente>(new NotFoundException($"Dipendente con id: {id} non trovato."));
            }

            this._appUnitOfWork.DipendenteRepository.Delete(objToDelete);
            await this._appUnitOfWork.SaveAsync();

            return objToDelete;
        }
    }
}
