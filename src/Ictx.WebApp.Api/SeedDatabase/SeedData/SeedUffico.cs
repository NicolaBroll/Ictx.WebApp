using Ictx.WebApp.Core.Entities;
using Ictx.WebApp.Infrastructure.UnitOfWork;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ictx.WebApp.Api.SeedDatabase.SeedData
{
    public class SeedUfficio: SeedDataCore
    {
        public SeedUfficio(AppUnitOfWork appUnitOfWork) : base(appUnitOfWork)
        { }

        public override async Task Popola()
        {
            await this._appUnitOfWork.UfficioRepository.InsertManyAsync(await GetUffici());
            await this._appUnitOfWork.SaveAsync();
        }

        private async Task<List<Ufficio>> GetUffici()
        {
            var result = new List<Ufficio>();
            var lstUfficiBase = await this._appUnitOfWork.UfficioBaseRepository.ReadManyAsync();

            foreach (var ufficioBase in lstUfficiBase)
            {
                foreach (var nRandom in Enumerable.Range(1, this._random.Next(1, 15)))
                {
                    result.Add(new Ufficio(nRandom, $"Sottoufficio {nRandom} - {ufficioBase.Denominazione}", ufficioBase));
                }
            }

            return result;
        }
    }
}
