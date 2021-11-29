using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using FluentValidation;
using Ictx.WebApp.Fwk.BO.Base;
using Ictx.WebApp.Fwk.Models;

namespace Ictx.WebApp.Core.Domain.Utente;

public class UtenteBO : PersistableBO<Utente, Guid, PaginationModel>
{
    public UtenteBO(IValidator<Utente> validator = null) : base(validator)
    { }

    protected async override Task<OperationResult<Utente>> ReadViewAsync(Guid key, CancellationToken cancellationToken)
    {
        return await Task.FromResult(OperationResult<Utente>.Success(new Utente
        {
            Id = key,
            Cognome = "Broll",
            Nome = "Nicola",
            Email = "nicola.brl94@gmail.com",
            LstDitteAllowed = new List<int> { 1 }
        }));
    }
}

