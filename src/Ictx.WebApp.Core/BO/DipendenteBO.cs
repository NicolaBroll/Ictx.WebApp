using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using FluentValidation;
using Ictx.WebApp.Core.Entities;
using Ictx.WebApp.Core.Models;
using Ictx.WebApp.Core.Contracts.UnitOfWork;
using System;

namespace Ictx.WebApp.Core.BO;

public class DipendenteBO: BaseBO<Dipendente, int, PaginationModel>
{
    private readonly IUserData _userData;

    public DipendenteBO(
        IAppUnitOfWork          appUnitOfWork,
        IValidator<Dipendente>  dipendenteValidator,
        IUserData               userData
        ) : base(appUnitOfWork, dipendenteValidator)
    {
        this._userData          = userData;
    }

    /// <summary>
    /// Ritorna una lista di dipendenti paginata.
    /// </summary>
    /// <param name="filter">Parametri di paginazione</param>
    /// <returns>Ritorna unoggetto contenente la lista di dipendenti paginata e il totalcount dei record su DB</returns>
    protected override async Task<PageResult<Dipendente>> ReadManyPaginatedViewsAsync(PaginationModel filter, CancellationToken cancellationToken)
    {
        var result = await this._appUnitOfWork.DipendenteRepository.ReadManyPaginatedAsync(
            pagination: filter,
            orderBy: x => x.OrderBy(o => o.Cognome).ThenBy(x => x.Nome),
            cancellationToken: cancellationToken);

        return result;
    }

    /// <summary>
    /// Ritorna un dipendente. Se non viene trovato, ritorna NotFoundException.
    /// </summary>
    /// <param name="key">Id dipendente</param>
    /// <returns>Ritorna un Result<Dipendente> contenente il dipendente associato all'id richiesto oppure una 
    /// DipendenteNotFoundException nel caso il dipendente non sia presente. </returns>
    protected override async Task<OperationResult<Dipendente>> ReadViewAsync(int key, CancellationToken cancellationToken)
    {
        var dipendente = await this._appUnitOfWork.DipendenteRepository.ReadAsync(key, cancellationToken);

        if (dipendente is null)
        {
            return OperationResult<Dipendente>.NotFound($"Dipendente con id: {key} non trovato.");
        }

        return OperationResult<Dipendente>.Success(dipendente);
    }

    /// <summary>
    /// Crea un dipendente.
    /// </summary>
    /// <param name="value">Modello contenente i dati del nuovo dipendente.</param>
    /// </returns>
    protected override async Task<OperationResult<Dipendente>> InsertViewAsync(Dipendente value, CancellationToken cancellationToken)
    {
        var utcNow = DateTime.UtcNow;

        value.InsertedUtc = utcNow;
        value.UpdatedUtc = utcNow;

        await this._appUnitOfWork.DipendenteRepository.InsertAsync(value, cancellationToken);
        await this._appUnitOfWork.SaveAsync(cancellationToken);

        return OperationResult<Dipendente>.Success(value);
    }


    /// <summary>
    /// Crea un dipendente.
    /// </summary>
    /// <param name="lstDipendenti">Modello contenente i dati del nuovo dipendente.</param>
    /// </returns>
    protected override async Task<OperationResult<List<Dipendente>>> InsertManyViewsAsync(List<Dipendente> lstDipendenti, CancellationToken cancellationToken)
    {
        var utcNow = DateTime.UtcNow;

        foreach (var dipendente in lstDipendenti)
        {
            dipendente.InsertedUtc = utcNow;
            dipendente.UpdatedUtc = utcNow;
        }

        await this._appUnitOfWork.DipendenteRepository.InsertManyAsync(lstDipendenti, cancellationToken);
        await this._appUnitOfWork.SaveAsync(cancellationToken);

        return OperationResult<List<Dipendente>>.Success(lstDipendenti);
    }

    /// <summary>
    /// Modifica un dipendente.
    /// </summary>
    /// <param name="key">Id dipendente da modificare.</param>
    /// <param name="value">Modello contenente i nuovi dati.</param>
    /// Se il dipendente non viene trovato, ritorna NotFoundException.
    /// </returns>
    protected override async Task<OperationResult<Dipendente>> SaveViewAsync(int key, Dipendente value, CancellationToken cancellationToken)
    {
        var objToUpdate = await this._appUnitOfWork.DipendenteRepository.ReadAsync(key, cancellationToken);

        if (objToUpdate is null)
        {
            return OperationResult<Dipendente>.NotFound($"Dipendente con id: {key} non trovato.");
        }

        objToUpdate.Nome = value.Nome;
        objToUpdate.Cognome = value.Cognome;
        objToUpdate.Sesso = value.Sesso;
        objToUpdate.DataNascita = value.DataNascita;
        objToUpdate.UpdatedUtc = DateTime.UtcNow;

        this._appUnitOfWork.DipendenteRepository.Update(objToUpdate);
        await this._appUnitOfWork.SaveAsync(cancellationToken);

        return OperationResult<Dipendente>.Success(objToUpdate);
    }

    /// <summary>
    /// Elimina un dipendente. Se non viene trovato, ritorna DipendenteNotFoundException.
    /// </summary>
    /// <param name="key">Id dipendente</param>
    protected override async Task<OperationResult<bool>> DeleteViewAsync(int key, CancellationToken cancellationToken)
    {
        var objToDelete = await this._appUnitOfWork.DipendenteRepository.ReadAsync(key, cancellationToken);

        if (objToDelete is null)
        {
            return OperationResult<bool>.NotFound($"Dipendente con id: {key} non trovato.");
        }

        objToDelete.IsDeleted = true;
        objToDelete.DeletedUtc = DateTime.UtcNow;

        this._appUnitOfWork.DipendenteRepository.Update(objToDelete);
        await this._appUnitOfWork.SaveAsync(cancellationToken);

        return OperationResult<bool>.Success(true);
    }
}