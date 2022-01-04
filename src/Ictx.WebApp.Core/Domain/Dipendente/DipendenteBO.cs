using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using FluentValidation;
using Ictx.WebApp.Core.Data.App;
using Ictx.WebApp.Core.Domain.Utente;
using Ictx.WebApp.Fwk.BO.Base;
using Ictx.WebApp.Fwk.Models;
using Ictx.WebApp.Fwk.Exceptions;

namespace Ictx.WebApp.Core.Domain.Dipendente;

public class DipendenteBO: PersistableBO<Dipendente, int, DipendenteFilter>
{
    private readonly IUserData      _userData;
    private readonly UtenteBO       _utenteBO;
    private readonly AppDbContext   _appDbContext;

    public DipendenteBO(
        AppDbContext            appDbContext,
        IValidator<Dipendente>  dipendenteValidator,
        IUserData               userData,
        UtenteBO                utenteBO
        ) : base(dipendenteValidator)
    {
        this._appDbContext  = appDbContext;
        this._userData      = userData;
        this._utenteBO      = utenteBO;
    }

    protected IQueryable<Dipendente> GetQuery(DipendenteFilter filter, Utente.Utente utente)
    {
        var query = this._appDbContext.Dipendente.AsQueryable();

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

        if (utente.Exception is not null)
        {
            return new PageResult<Dipendente>();
        }

        var query = GetQuery(filter, utente.Data);

        return await GetPaginatedResult(query, filter);
    }

    /// <summary>
    /// Ritorna un dipendente. Se non viene trovato, ritorna NotFoundException.
    /// </summary>
    /// <param name="key">Id dipendente</param>
    /// <returns>Ritorna un Result<Dipendente> contenente il dipendente associato all'id richiesto oppure una 
    /// DipendenteNotFoundException nel caso il dipendente non sia presente. </returns>
    protected override async Task<(Dipendente, Exception)> ReadViewAsync(int key, CancellationToken cancellationToken)
    {
        var dipendente = await this._appDbContext.Dipendente.Where(x => x.Id == key).FirstOrDefaultAsync();

        if (dipendente is null)
        {
            return (null, new NotFoundException($"Dipendente con id: {key} non trovato."));
        }

        if ((await IsAllowedToViewDipendente(key, cancellationToken)))
        {
            return (null, new UnauthorizedException());
        }

        return (dipendente, null);
    }

    /// <summary>
    /// Crea un dipendente.
    /// </summary>
    /// <param name="value">Modello contenente i dati del nuovo dipendente.</param>
    /// </returns>
    protected override async Task<(Dipendente Data, Exception Exception)> InsertViewAsync(Dipendente value, CancellationToken cancellationToken)
    {
        var utcNow = DateTime.UtcNow;

        value.InsertedUtc = utcNow;
        value.UpdatedUtc = utcNow;

        await this._appDbContext.Dipendente.AddAsync(value, cancellationToken);
        await this._appDbContext.SaveChangesAsync(cancellationToken);

        return (value, null);
    }


    /// <summary>
    /// Crea un dipendente.
    /// </summary>
    /// <param name="lstDipendenti">Modello contenente i dati del nuovo dipendente.</param>
    /// </returns>
    protected override async Task<(List<Dipendente> Data, Exception Exception)> InsertManyViewsAsync(List<Dipendente> lstDipendenti, CancellationToken cancellationToken)
    {
        var utcNow = DateTime.UtcNow;

        foreach (var dipendente in lstDipendenti)
        {
            dipendente.InsertedUtc = utcNow;
            dipendente.UpdatedUtc = utcNow;
        }

        await this._appDbContext.Dipendente.AddRangeAsync(lstDipendenti, cancellationToken);
        await this._appDbContext.SaveChangesAsync(cancellationToken);

        return (lstDipendenti, null);
    }

    /// <summary>
    /// Modifica un dipendente.
    /// </summary>
    /// <param name="key">Id dipendente da modificare.</param>
    /// <param name="value">Modello contenente i nuovi dati.</param>
    /// Se il dipendente non viene trovato, ritorna NotFoundException.
    /// </returns>
    protected override async Task<(Dipendente Data, Exception Exception)> SaveViewAsync(int key, Dipendente value, CancellationToken cancellationToken)
    {
        var dipendente = await this._appDbContext.Dipendente.Where(x => x.Id == key).FirstOrDefaultAsync();

        if (dipendente is null)
        {
            return (null, new NotFoundException($"Dipendente con id: {key} non trovato."));
        }

        if ((await IsAllowedToViewDipendente(key, cancellationToken)))
        {
            return (null, new UnauthorizedException());
        }

        dipendente.Nome = value.Nome;
        dipendente.Cognome = value.Cognome;
        dipendente.Sesso = value.Sesso;
        dipendente.DataNascita = value.DataNascita;
        dipendente.UpdatedUtc = DateTime.UtcNow;

        this._appDbContext.Dipendente.Update(dipendente);
        await this._appDbContext.SaveChangesAsync(cancellationToken);

        return (dipendente, null);
    }

    /// <summary>
    /// Elimina un dipendente. Se non viene trovato, ritorna DipendenteNotFoundException.
    /// </summary>
    /// <param name="key">Id dipendente</param>
    protected override async Task<(bool Data, Exception Exception)> DeleteViewAsync(int key, CancellationToken cancellationToken)
    {
        var dipendente = await this._appDbContext.Dipendente.Where(x => x.Id == key).FirstOrDefaultAsync();

        if (dipendente is null)
        {
            return (false, new NotFoundException($"Dipendente con id: {key} non trovato."));
        }

        if ((await IsAllowedToViewDipendente(key, cancellationToken)))
        {
            return (false, new UnauthorizedException());
        }

        dipendente.IsDeleted = true;
        dipendente.DeletedUtc = DateTime.UtcNow;

        this._appDbContext.Dipendente.Update(dipendente);
        await this._appDbContext.SaveChangesAsync(cancellationToken);

        return (true, null);
    }

    private async Task<bool> IsAllowedToViewDipendente(int key, CancellationToken cancellationToken)
    {
        var utente = await this._utenteBO.ReadAsync(this._userData.UserId);

        if (utente.Exception is not null)
        {
            return false;
        }

        var query = GetQuery(new DipendenteFilter
        {
            Id = key,
        }, utente.Data);

        var dipendente = await query.FirstOrDefaultAsync(cancellationToken);

        return dipendente is null;
    }
}