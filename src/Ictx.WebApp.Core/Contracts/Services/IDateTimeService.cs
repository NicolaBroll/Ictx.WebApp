using System;

namespace Ictx.WebApp.Core.Contracts.Services
{
    public interface IDateTimeService
    {
        DateTime Now { get; }
        DateTime UtcNow { get; }
    }
}
