using System;

namespace Ictx.WebApp.Infrastructure.Services.Interfaces
{
    public interface IDateTimeService
    {
        DateTime Now { get; }
        DateTime UtcNow { get; }
    }
}