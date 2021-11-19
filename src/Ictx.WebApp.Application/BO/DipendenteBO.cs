using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using Ictx.WebApp.Core.Entities;
using Ictx.WebApp.Core.Exceptions;
using Ictx.WebApp.Application.Models;
using Ictx.WebApp.Application.Validators;
using Ictx.WebApp.Application.Contracts.UnitOfWork;

namespace Ictx.WebApp.Application.BO;

public class DipendenteBO
{
    private readonly IAppUnitOfWork _appUnitOfWork;

    public DipendenteBO(IAppUnitOfWork appUnitOfWork)
    {
        this._appUnitOfWork = appUnitOfWork;
    }

    /// <summary>
    /// Ritorna una lista di dipendenti paginata.
    /// </summary>
    /// <param name="filter">Parametri di paginazione</param>
    /// <returns>Ritorna unoggetto contenente la lista di dipendenti paginata e il totalcount dei record su DB</returns>
    public async Task<PageResult<Dipendente>> ReadManyPaginatedAsync(PaginationModel filter, CancellationToken cancellationToken)
    {
        var result = await this._appUnitOfWork.DipendenteRepository.ReadManyPaginatedAsync(
            pagination: filter,
            orderBy: x => x.OrderBy(o => o.Cognome).ThenBy(x => x.Nome),
            cancellationToken: cancellationToken);

        return result;
    }

    /// <summary>
    /// Ritorna un dipendente. Se non viene trovato, ritorna DipendenteNotFoundException.
    /// </summary>
    /// <param name="key">Id dipendente</param>
    /// <returns>Ritorna un Result<Dipendente> contenente il dipendente associato all'id richiesto oppure una 
    /// DipendenteNotFoundException nel caso il dipendente non sia presente. </returns>
    public async Task<OperationResult<Dipendente>> ReadAsync(int key, CancellationToken cancellationToken)
    {
        var dipendente = await this._appUnitOfWork.DipendenteRepository.ReadAsync(key, cancellationToken);

        if (dipendente is null)
        {
            return OperationResult<Dipendente>.Fail(new NotFoundException($"Dipendente con id: {key} non trovato."));
        }

        return OperationResult<Dipendente>.Success(dipendente);
    }

    /// <summary>
    /// Crea un dipendente.
    /// </summary>
    /// <param name="value">Modello contenente i dati del nuovo dipendente.</param>
    /// <returns>Ritorna un Result<Dipendente> contenente il dipendente creato.
    /// Se il dipendente non viene trovato, ritorna DipendenteNotFoundException.
    /// </returns>
    public async Task<OperationResult<Dipendente>> InsertAsync(Dipendente value, CancellationToken cancellationToken)
    {
        var insert = await Insert(value, cancellationToken);

        if (insert.IsFail)
        {
            return insert;
        }

        await this._appUnitOfWork.SaveAsync(cancellationToken);

        return OperationResult<Dipendente>.Success(value);
    }

    private async Task<OperationResult<Dipendente>> Insert(Dipendente value, CancellationToken cancellationToken)
    {
        var validazione = Validation(value);

        if (validazione.IsFail)
        {
            return validazione;
        }

        await this._appUnitOfWork.DipendenteRepository.InsertAsync(value, cancellationToken);

        return OperationResult<Dipendente>.Success(value);
    }

    /// <summary>
    /// Crea un dipendente.
    /// </summary>
    /// <param name="lstDipendenti">Modello contenente i dati del nuovo dipendente.</param>
    /// <returns>Ritorna un Result<Dipendente> contenente il dipendente creato.
    /// Se il dipendente non viene trovato, ritorna DipendenteNotFoundException.
    /// </returns>
    public async Task<OperationResult<List<Dipendente>>> InsertManyAsync(List<Dipendente> lstDipendenti, CancellationToken cancellationToken)
    {
        foreach (var dipendente in lstDipendenti)
        {
            var validazione = await Insert(dipendente, cancellationToken);

            if (validazione.IsFail)
            {
                return OperationResult<List<Dipendente>>.Fail(validazione.Exception);
            }
        }

        await this._appUnitOfWork.SaveAsync(cancellationToken);

        return OperationResult<List<Dipendente>>.Success(lstDipendenti);
    }

    /// <summary>
    /// Modifica un dipendente.
    /// </summary>
    /// <param name="key">Id dipendente da modificare.</param>
    /// <param name="value">Modello contenente i nuovi dati.</param>
    /// <returns>Ritorna un Result<Dipendente> contenente il dipendente modificato.
    /// Se il dipendente non viene trovato, ritorna DipendenteNotFoundException.
    /// </returns>
    public async Task<OperationResult<Dipendente>> SaveAsync(int key, Dipendente value, CancellationToken cancellationToken)
    {
        var validazione = Validation(value);

        if (validazione.IsFail)
        {
            return validazione;
        }

        var objToUpdate = await this._appUnitOfWork.DipendenteRepository.ReadAsync(key, cancellationToken);

        if (objToUpdate is null)
        {
            return OperationResult<Dipendente>.Fail(new NotFoundException($"Dipendente con id: {key} non trovato."));
        }

        objToUpdate.Nome = value.Nome;
        objToUpdate.Cognome = value.Cognome;
        objToUpdate.Sesso = value.Sesso;
        objToUpdate.DataNascita = value.DataNascita;

        this._appUnitOfWork.DipendenteRepository.Update(objToUpdate);
        await this._appUnitOfWork.SaveAsync(cancellationToken);

        return OperationResult<Dipendente>.Success(objToUpdate);
    }

    /// <summary>
    /// Elimina un dipendente. Se non viene trovato, ritorna DipendenteNotFoundException.
    /// </summary>
    /// <param name="key">Id dipendente</param>
    /// <returns>Ritorna un Result<Dipendente> contenente il dipendente eliminato. Oppure una 
    /// DipendenteNotFoundException nel caso il dipendente non sia presente. </returns>
    public async Task<OperationResult<bool>> DeleteAsync(int key, CancellationToken cancellationToken)
    {
        var objToDelete = await this._appUnitOfWork.DipendenteRepository.ReadAsync(key, cancellationToken);

        if (objToDelete is null)
        {
            return OperationResult<bool>.Fail(new NotFoundException($"Dipendente con id: {key} non trovato."));
        }

        objToDelete.IsDeleted = true;

        this._appUnitOfWork.DipendenteRepository.Update(objToDelete);
        await this._appUnitOfWork.SaveAsync(cancellationToken);

        return OperationResult<bool>.Success(true);
    }

    protected OperationResult<Dipendente> Validation(Dipendente value)
    {
        var validationResult = new DipendenteValidator().Validate(value);

        if (validationResult.IsValid)
        {
            return OperationResult<Dipendente>.Success(value);
        }

        var dictionaryErrors = validationResult.Errors
            .GroupBy(x => x.PropertyName)
            .ToDictionary(k => k.Key, v => v.Select(x => x.ErrorMessage));

        return OperationResult<Dipendente>.Fail(new BadRequestException(errors: dictionaryErrors));
    }
}