using System.Threading.Tasks;
using System.Collections.Generic;
using Ictx.WebApp.Core.Entities;
using Ictx.WebApp.Infrastructure.UnitOfWork;

namespace Ictx.WebApp.Api.Data.SeedData
{
    public class SeedUfficioBase : SeedDataCore
    {
        public SeedUfficioBase(AppUnitOfWork appUnitOfWork) : base(appUnitOfWork)
        { }

        public override async Task Popola()
        {
            await this._appUnitOfWork.UfficioBaseRepository.InsertManyAsync(GetUfficiBase());
            await this._appUnitOfWork.SaveAsync();
        }

        private List<UfficioBase> GetUfficiBase()
        {
            return new List<UfficioBase>()
            {
                new UfficioBase(1, "Ufficio base test")
            };
        }
    }
}
