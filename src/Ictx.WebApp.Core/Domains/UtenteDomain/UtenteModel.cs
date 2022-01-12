using System;
using System.Collections.Generic;

namespace Ictx.WebApp.Core.Domain.UtenteDomain;

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
    string UfficioBase { get; }
}

public class UserData : IUserData
{
    public Guid UserId { get; private set; }
    public string UfficioBase { get; private set; }

    public UserData(Guid userId, string ufficioBase)
    {
        this.UserId = userId;
        this.UfficioBase = ufficioBase;
    }
}
