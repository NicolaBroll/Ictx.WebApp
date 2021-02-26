using System;
using System.IO;
using System.Threading.Tasks;
using Ictx.WebApp.Infrastructure.UnitOfWork;

namespace Ictx.WebApp.Api.Database.SeedData
{
    public partial class SeedDataCore
    {
        protected readonly AppUnitOfWork _appUnitOfWork;
        protected readonly string _seedStaticDataDirectory;
        protected readonly Random _random;

        public SeedDataCore(AppUnitOfWork appUnitOfWork)
        {
            this._appUnitOfWork = appUnitOfWork;
            this._seedStaticDataDirectory = Path.Combine(Directory.GetCurrentDirectory(), "SeedDatabase", "SeedData", "Static");
            this._random = new Random();
        }

        public virtual async Task Popola()
        {
            await Task.CompletedTask;
        }
    }
}
