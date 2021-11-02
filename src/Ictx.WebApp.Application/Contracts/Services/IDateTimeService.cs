using System;

namespace Ictx.WebApp.Application.Contracts.Services
{
    public interface IDateTimeService
    {
        DateTime Now { get; }
        DateTime UtcNow { get; }
    }
}
