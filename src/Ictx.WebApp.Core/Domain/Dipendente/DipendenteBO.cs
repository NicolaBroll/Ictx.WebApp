using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using FluentValidation;
using Ictx.WebApp.Core.Models;
using Ictx.WebApp.Core.BO.Base;
using Ictx.WebApp.Core.Data.App;
using Ictx.WebApp.Core.Domain.Utente;

namespace Ictx.WebApp.Core.Domain.Dipendente;

public class DipendenteBO: PersistableBO<Dipendente, int, DipendenteFilter>
{
    private readonly IUserData  _userData;
    private readonly UtenteBO   _utenteBO;

    public DipendenteBO(
        AppDbContext            appDbContext,
        IValidator<Dipendente>  dipendenteValidator,
        IUserData               userData,
        UtenteBO                utenteBO
        ) : base(appDbContext, dipendenteValidator)
    {
        this._userData = userData;
        this._utenteBO = utenteBO;
    }

    protected IQueryable<Dipendente> GetQuery(DipendenteFilter filter, Utente.Utente utente)
    {
        var query = this.AppDbContext.Dipendente.AsQueryable();

        query = ApplicaFiltri(query, filter);
        query = ApplicaFiltriUtente(query, utente);

        return query;
    }

    private IQueryable<Dipendente> ApplicaFiltriUtente(IQueryable<Dipendente> query, Utente.Utente utente)
    {
        query = query.Where(x => utente.LstDitteAllowed.Contains(x.IdDitta));

        return query;
    }

    private IQueryable<Dipendente> ApplicaFiltri(IQueryable<Dipendente> query, DipendenteFilter filter)
    {
        if (filter.Id != null)
        {
            query = query.Where(x => x.Id == filter.Id);
        }

        if (filter.IdDitta != null)
        {
            query = query.Where(x => x.IdDitta == filter.IdDitta);
        }

        return query;
    }


    /// <summary>
    /// Ritorna una lista di dipendenti paginata.
    /// </summary>
    /// <param name="filter">Parametri di paginazione</param>
    /// <returns>Ritorna unoggetto contenente la lista di dipendenti paginata e il totalcount dei record su DB</returns>
    protected override async Task<PageResult<Dipendente>> ReadManyPaginatedViewsAsync(DipendenteFilter filter, CancellationToken cancellationToken)
    {
        var utente = await this._utenteBO.ReadAsync(this._userData.UserId);

        if (utente.IsFail)
        {
            return new PageResult<Dipendente>();
        }

        var query = GetQuery(filter, utente.ResultData);

        return await GetPaginatedResult(query, filter);
    }

    /// <summary>
    /// Ritorna un dipendente. Se non viene trovato, ritorna NotFoundException.
    /// </summary>
    /// <param name="key">Id dipendente</param>
    /// <returns>Ritorna un Result<Dipendente> contenente il dipendente associato all'id richiesto oppure una 
    /// DipendenteNotFoundException nel caso il dipendente non sia presente. </returns>
    protected override async Task<OperationResult<Dipendente>> ReadViewAsync(int key, CancellationToken cancellationToken)
    {
        var dipendente = await this.AppDbContext.Dipendente.Where(x => x.Id == key).FirstOrDefaultAsync();

        if (dipendente is null)
        {
            return OperationResult<Dipendente>.NotFound($"Dipendente con id: {key} non trovato.");
        }

        if ((await IsAllowedToViewDipendente(key, cancellationToken)).IsFail)
        {
            return OperationResult<Dipendente>.Unauthorized();
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

        await this.AppDbContext.Dipendente.AddAsync(value, cancellationToken);
        await this.AppDbContext.SaveChangesAsync(cancellationToken);

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

        await this.AppDbContext.Dipendente.AddRangeAsync(lstDipendenti, cancellationToken);
        await this.AppDbContext.SaveChangesAsync(cancellationToken);

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
        var dipendente = await this.AppDbContext.Dipendente.Where(x => x.Id == key).FirstOrDefaultAsync();

        if (dipendente is null)
        {
            return OperationResult<Dipendente>.NotFound($"Dipendente con id: {key} non trovato.");
        }

        if ((await IsAllowedToViewDipendente(key, cancellationToken)).IsFail)
        {
            return OperationResult<Dipendente>.Unauthorized();
        }

        dipendente.Nome = value.Nome;
        dipendente.Cognome = value.Cognome;
        dipendente.Sesso = value.Sesso;
        dipendente.DataNascita = value.DataNascita;
        dipendente.UpdatedUtc = DateTime.UtcNow;

        this.AppDbContext.Dipendente.Update(dipendente);
        await this.AppDbContext.SaveChangesAsync(cancellationToken);

        return OperationResult<Dipendente>.Success(dipendente);
    }

    /// <summary>
    /// Elimina un dipendente. Se non viene trovato, ritorna DipendenteNotFoundException.
    /// </summary>
    /// <param name="key">Id dipendente</param>
    protected override async Task<OperationResult<bool>> DeleteViewAsync(int key, CancellationToken cancellationToken)
    {
        var dipendente = await this.AppDbContext.Dipendente.Where(x => x.Id == key).FirstOrDefaultAsync();

        if (dipendente is null)
        {
            return OperationResult<bool>.NotFound($"Dipendente con id: {key} non trovato.");
        }

        if ((await IsAllowedToViewDipendente(key, cancellationToken)).IsFail)
        {
            return OperationResult<bool>.Unauthorized();
        }

        dipendente.IsDeleted = true;
        dipendente.DeletedUtc = DateTime.UtcNow;

        this.AppDbContext.Dipendente.Update(dipendente);
        await this.AppDbContext.SaveChangesAsync(cancellationToken);

        return OperationResult<bool>.Success(true);
    }

    private async Task<OperationResult<Dipendente>> IsAllowedToViewDipendente(int key, CancellationToken cancellationToken)
    {
        var utente = await this._utenteBO.ReadAsync(this._userData.UserId);

        if (utente.IsFail)
        {
            return OperationResult<Dipendente>.Unauthorized();
        }

        var query = GetQuery(new DipendenteFilter
        {
            Id = key,
        }, utente.ResultData);

        var dipendente = await query.FirstOrDefaultAsync(cancellationToken);

        return dipendente is null ? OperationResult<Dipendente>.Unauthorized() : OperationResult<Dipendente>.Success(dipendente);
    }
}