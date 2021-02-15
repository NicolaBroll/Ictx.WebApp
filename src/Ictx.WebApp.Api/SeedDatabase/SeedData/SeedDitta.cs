using Ictx.WebApp.Core.Entities;
using Ictx.WebApp.Infrastructure.UnitOfWork;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ictx.WebApp.Api.Database.SeedData
{
    public class SeedDitta : SeedDataCore
    {
        public SeedDitta(AppUnitOfWork appUnitOfWork) : base(appUnitOfWork)
        { }

        public override async Task Popola()
        {
            await this._appUnitOfWork.DittaRepository.InsertManyAsync(await GetDitte());
            await this._appUnitOfWork.SaveAsync();
        }

        private async Task<List<Ditta>> GetDitte()
        {
            var result = new List<Ditta>();
            var lstImprese = await this._appUnitOfWork.ImpresaRepository.ReadManyAsync();

            foreach (var impresa in lstImprese)
            {
                foreach (var nRandom in Enumerable.Range(1, this._random.Next(1, 5)))
                {
                    result.Add(new Ditta(nRandom, $"Ditta {nRandom}", impresa));
                }
            }

            return result;
        }
    }
}
