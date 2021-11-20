using Ictx.WebApp.Core.Contracts.Services;
using System;

namespace Ictx.WebApp.Infrastructure.Services
{
    public class DateTimeService : IDateTimeService
    {
        public DateTime Now => DateTime.Now;
        public DateTime UtcNow => DateTime.Now;
    }
}
