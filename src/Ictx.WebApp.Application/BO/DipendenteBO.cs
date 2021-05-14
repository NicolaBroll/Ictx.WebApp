using System.Linq;
using System.Threading.Tasks;
using Ictx.WebApp.Core.Entities;
using Ictx.WebApp.Core.Exceptions.Dipendente;
using Ictx.WebApp.Core.Models;
using Ictx.WebApp.Infrastructure.UnitOfWork;
using Microsoft.Extensions.Logging;

namespace Ictx.WebApp.Application.BO
{
    public class DipendenteBO : BaseBO<Dipendente, int, PaginationModel>
    {
        private readonly IAppUnitOfWork     _appUnitOfWork;
        private readonly ISessionData       _sessionData;

        public DipendenteBO(ILogger<DipendenteBO> logger,
            IAppUnitOfWork appUnitOfWork,
            ISessionData sessionData): base(logger)
        {
            this._appUnitOfWork     = appUnitOfWork;
            this._sessionData       = sessionData;
        }

        /// <summary>
        /// Ritorna una lista di dipendenti paginata.
        /// </summary>
        /// <param name="filter">Parametri di paginazione</param>
        /// <returns>Ritorna unoggetto contenente la lista di dipendenti paginata e il totalcount dei record su DB</returns>
        protected override async Task<PageResult<Dipendente>> ReadManyViewsAsync(PaginationModel filter)
        {
            var result = await this._appUnitOfWork.DipendenteRepository.ReadManyPaginatedAsync(
                pagination: filter,
                orderBy: x => x.OrderBy(o => o.Cognome).ThenBy(x => x.Nome));

            return result;
        }

        /// <summary>
        /// Ritorna un dipendente. Se non viene trovato, ritorna DipendenteNotFoundException.
        /// </summary>
        /// <param name="key">Id dipendente</param>
        /// <returns>Ritorna un Result<Dipendente> contenente il dipendente associato all'id richiesto oppure una 
        /// DipendenteNotFoundException nel caso il dipendente non sia presente. </returns>
        protected override async Task<OperationResult<Dipendente>> ReadViewAsync(int key)
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
        protected override async Task<OperationResult<Dipendente>> InsertViewAsync(Dipendente value)
        {
            var objToInsert = new Dipendente
            {
                CodiceFiscale = value.CodiceFiscale,
                Cognome = value.Cognome,
                Nome = value.Nome,
                Sesso = value.Sesso,
                DataNascita = value.DataNascita
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
        protected override async Task<OperationResult<Dipendente>> SaveViewAsync(int key, Dipendente value)
        {
            var objToUpdate = await this._appUnitOfWork.DipendenteRepository.ReadAsync(key);

            if (objToUpdate is null)
            {
                return new OperationResult<Dipendente>(new NotFoundException($"Dipendente con id: {key} non trovato."));
            }

            objToUpdate.CodiceFiscale = value.CodiceFiscale;
            objToUpdate.Nome = value.Nome;
            objToUpdate.Cognome = value.Cognome;
            objToUpdate.Sesso = value.Sesso;
            objToUpdate.DataNascita = value.DataNascita;

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
        protected override async Task<OperationResult<bool>> DeleteViewAsync(int key)
        {
            var objToDelete = await this._appUnitOfWork.DipendenteRepository.ReadAsync(key);

            if (objToDelete is null)
            {
                return new OperationResult<bool>(new NotFoundException($"Dipendente con id: {key} non trovato."));
            }

            objToDelete.IsDeleted = true;

            this._appUnitOfWork.DipendenteRepository.Update(objToDelete);
            await this._appUnitOfWork.SaveAsync();

            return new OperationResult<bool>(true);
        }
    }
}
