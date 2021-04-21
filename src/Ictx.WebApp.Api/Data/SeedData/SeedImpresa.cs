using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Ictx.WebApp.Core.Entities;
using Ictx.WebApp.Infrastructure.UnitOfWork;

namespace Ictx.WebApp.Api.Data.SeedData
{
    public class SeedImpresa: SeedDataCore
    {
        public SeedImpresa(AppUnitOfWork appUnitOfWork) : base(appUnitOfWork)
        { }

        public override async Task Popola()
        {
            await this._appUnitOfWork.ImpresaRepository.InsertManyAsync(await GetImprese());
            await this._appUnitOfWork.SaveAsync();
        }

        private async Task<List<Impresa>> GetImprese()
        {
            var result = new List<Impresa>();
            var lstUffici = await this._appUnitOfWork.UfficioRepository.ReadManyAsync();

            foreach (var ufficio in lstUffici)
            {
                foreach (var nRandom in Enumerable.Range(1, this._random.Next(1, 5)))
                {
                    result.Add(new Impresa(nRandom, $"Impresa {nRandom}", ufficio));
                }
            }

            return result;
        }

    }
}
