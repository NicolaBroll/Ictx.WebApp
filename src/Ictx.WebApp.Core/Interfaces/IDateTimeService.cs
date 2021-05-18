using System;

namespace Ictx.WebApp.Core.Interfaces
{
    public interface IDateTimeService
    {
        DateTime Now { get; }
        DateTime UtcNow { get; }
    }
}
