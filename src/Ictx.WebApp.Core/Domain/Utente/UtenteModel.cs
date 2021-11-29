using System;
using System.Collections.Generic;

namespace Ictx.WebApp.Core.Domain.Utente;

public class Utente
{
    public Guid Id { get; set; }
    public string Nome { get; set; }
    public string Cognome { get; set; }
    public string Email { get; set; }
    public List<int> LstDitteAllowed { get; set; }
}

public interface IUserData
{
    Guid UserId { get; }
}

public class UserData : IUserData
{
    public Guid UserId { get; private set; }

    public UserData(Guid userId)
    {
        this.UserId = userId;
    }
}
