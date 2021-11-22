using System;

namespace Ictx.WebApp.Core.Models;

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