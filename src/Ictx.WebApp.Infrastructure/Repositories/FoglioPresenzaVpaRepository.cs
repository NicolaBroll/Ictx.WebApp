using Ictx.WebApp.Core.Entities;
using Ictx.WebApp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Ictx.WebApp.Infrastructure.Repositories
{
    public class FoglioPresenzaVpaRepository : GenericRepository<FoglioPresenzaVpa>
    {
        public FoglioPresenzaVpaRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<FoglioPresenzaVpa> GetBySigla(string sigla)
        {
            return await QueryMany(filter: x => x.Sigla == sigla).FirstOrDefaultAsync();
        }
    }    
}
