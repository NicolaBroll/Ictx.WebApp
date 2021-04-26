using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Ictx.WebApp.Core.Entities;
using Ictx.WebApp.Infrastructure.UnitOfWork;

namespace Ictx.WebApp.Api.Data.SeedData
{
    public class SeedDitta : SeedDataCore
    {
        public SeedDitta(IAppUnitOfWork appUnitOfWork) : base(appUnitOfWork)
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
                foreach (var nRandom in Enumerable.Range(1, this._random.Next(1, 3)))
                {
                    result.Add(new Ditta(nRandom, $"Ditta {nRandom}", impresa));
                }
            }

            return result;
        }
    }
}
