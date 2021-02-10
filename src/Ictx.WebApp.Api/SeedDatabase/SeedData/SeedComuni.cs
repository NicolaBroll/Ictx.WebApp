using Ictx.WebApp.Core.Entities;
using Ictx.WebApp.Infrastructure.UnitOfWork;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Ictx.WebApp.Api.SeedDatabase.SeedData
{
    public class SeedComuni : SeedDataCore
    {
        public SeedComuni(AppUnitOfWork context) : base(context)
        { }

        public override async Task Popola()
        {
            await this._appUnitOfWork.ComuneRepository.InsertManyAsync(await GetComuni());
            await this._appUnitOfWork.SaveAsync();
        }

        private async Task<List<Comune>> GetComuni()
        {
            var result = new List<Comune>();            
            var lstComuni = await File.ReadAllLinesAsync(Path.Combine(this._seedStaticDataDirectory, "Comuni.txt"));

            foreach (var comuneStr in lstComuni)
            {
                var comuneSplit = comuneStr.Split(";");
                var comune = new Comune(comuneSplit[0], comuneSplit[1], comuneSplit[2]);
                result.Add(comune);
            }

            return result;
        }
    }
}
