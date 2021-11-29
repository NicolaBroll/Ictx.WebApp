using System;

namespace Ictx.WebApp.Core.Services
{
    public interface IDateTimeService
    {
        DateTime Now { get; }
        DateTime UtcNow { get; }
    }

    public class DateTimeService : IDateTimeService
    {
        public DateTime Now => DateTime.Now;

        public DateTime UtcNow => DateTime.UtcNow;
    }
}
