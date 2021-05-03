using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Ictx.WebApp.Core.Models;
using Ictx.WebApp.Core.Entities;
using Ictx.WebApp.Core.Interfaces;
using Ictx.WebApp.Core.Exceptions.Dipendente;
using Ictx.WebApp.Infrastructure.UnitOfWork;

namespace Ictx.WebApp.Infrastructure.Services
{
    public class DipendenteService : IDipendenteService
    {
        private readonly IAppUnitOfWork _appUnitOfWork;
        private readonly IDateTimeService _dateTimeService;

        public DipendenteService(IAppUnitOfWork appUnitOfWork, IDateTimeService dateTimeService)
        {
            this._appUnitOfWork = appUnitOfWork;
            this._dateTimeService = dateTimeService;
        }

        /// <summary>
        /// Ritorna una lista di dipendenti paginata per una determinata ditta.
        /// </summary>
        /// <param name="filter">Id della ditta e parametri di paginazione</param>
        /// <returns>Ritorna unoggetto contenente la lista di dipendenti paginata e il totalcount dei record su DB</returns>
        public async Task<PageResult<Dipendente>> ReadManyAsync(DipendenteListFilter filter)
        {
            var query = this._appUnitOfWork.DipendenteRepository.QueryMany(
                filter: x => x.DittaId == filter.DittaId,
                orderBy: x => x.OrderBy(o => o.Cognome).ThenBy(x => x.Nome));

            // Pagination.
            var count = query.Count();
            var list = await query.Skip((filter.Page - 1) * filter.PageSize).Take(filter.PageSize).ToListAsync();

            return new PageResult<Dipendente>(list, count);
        }

        /// <summary>
        /// Ritorna un dipendente. Se non viene trovato, ritorna DipendenteNotFoundException.
        /// </summary>
        /// <param name="key">Id dipendente</param>
        /// <returns>Ritorna un Result<Dipendente> contenente il dipendente associato all'id richiesto oppure una 
        /// DipendenteNotFoundException nel caso il dipendente non sia presente. </returns>
        public async Task<OperationResult<Dipendente>> ReadAsync(int key)
        {
            var dipendente = await this._appUnitOfWork.DipendenteRepository.ReadAsync(key);

            if (dipendente is null)
            {
                return new OperationResult<Dipendente>(new NotFoundException($"Dipendente con id: {key} non trovato."));
            }

            return new OperationResult<Dipendente>(dipendente);
        }

        /// <summary>
        /// Crea un dipendente.
        /// </summary>
        /// <param name="value">Modello contenente i dati del nuovo dipendente.</param>
        /// <returns>Ritorna un Result<Dipendente> contenente il dipendente creato.
        /// Se il dipendente non viene trovato, ritorna DipendenteNotFoundException.
        /// Se la ditta non viene trovata, ritorna DittaNotFoundException.
        /// </returns>
        public async Task<OperationResult<Dipendente>> InsertAsync(Dipendente value)
        {
            // Leggo la ditta.
            var ditta = await this._appUnitOfWork.DittaRepository.ReadAsync(value.DittaId);

            if (ditta is null)
            {
                return new OperationResult<Dipendente>(new BadRequestException("Impossibile associare il dipendente alla ditta richiesta."));
            }

            var utcNow = this._dateTimeService.UtcNow;

            var objToInsert = new Dipendente
            {
                DittaId = ditta.Id,
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

            return new OperationResult<Dipendente>(objToInsert);
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
        public async Task<OperationResult<Dipendente>> SaveAsync(int key, Dipendente value)
        {
            var objToUpdate = await this._appUnitOfWork.DipendenteRepository.ReadAsync(key);

            if (objToUpdate is null)
            {
                return new OperationResult<Dipendente>(new NotFoundException($"Dipendente con id: {key} non trovato."));
            }

            // Leggo la ditta.
            var ditta = await this._appUnitOfWork.DittaRepository.ReadAsync(value.DittaId);

            if (ditta is null)
            {
                return new OperationResult<Dipendente>(new BadRequestException("Impossibile associare il dipendente alla ditta richiesta."));
            }

            objToUpdate.CodiceFiscale = value.CodiceFiscale.ToUpper();
            objToUpdate.Nome = value.Nome.ToUpper();
            objToUpdate.Cognome = value.Cognome.ToUpper();
            objToUpdate.Sesso = value.Sesso;
            objToUpdate.DataNascita = value.DataNascita;
            objToUpdate.Updated = this._dateTimeService.UtcNow;
            objToUpdate.DittaId = ditta.Id;

            this._appUnitOfWork.DipendenteRepository.Update(objToUpdate);
            await this._appUnitOfWork.SaveAsync();

            return new OperationResult<Dipendente>(objToUpdate);
        }

        /// <summary>
        /// Elimina un dipendente. Se non viene trovato, ritorna DipendenteNotFoundException.
        /// </summary>
        /// <param name="id">Id dipendente</param>
        /// <returns>Ritorna un Result<Dipendente> contenente il dipendente eliminato. Oppure una 
        /// DipendenteNotFoundException nel caso il dipendente non sia presente. </returns>
        public async Task<OperationResult<bool>> DeleteAsync(int key)
        {
            var objToDelete = await this._appUnitOfWork.DipendenteRepository.ReadAsync(key);

            if (objToDelete is null)
            {
                return new OperationResult<bool>(new NotFoundException($"Dipendente con id: {key} non trovato."));
            }

            this._appUnitOfWork.DipendenteRepository.Delete(objToDelete);
            await this._appUnitOfWork.SaveAsync();

            return new OperationResult<bool>(true);
        }
    }
}
