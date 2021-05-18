using Ictx.WebApp.Core.Interfaces;
using System;

namespace Ictx.WebApp.Infrastructure.Application
{
    public class DateTimeService : IDateTimeService
    {
        public DateTime Now => DateTime.Now;
        public DateTime UtcNow => DateTime.Now;
    }
}
