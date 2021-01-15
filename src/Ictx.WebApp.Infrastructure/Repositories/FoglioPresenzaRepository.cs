using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Ictx.WebApp.Core.Entities;
using Ictx.WebApp.Infrastructure.Data;

namespace Ictx.WebApp.Infrastructure.Repositories
{
    public class FoglioPresenzaRepository : GenericRepository<FoglioPresenza>
    {
        public FoglioPresenzaRepository(AppDbContext context) : base(context)
        { }

        internal async Task<FoglioPresenza> GetFoglioPresenza(int dipendenteId, int anno, int mese)
        {
            return await QueryMany(
                filter: x => x.DipendenteId == dipendenteId && x.Anno == anno && x.Mese == mese,
                includeProperties: "Giorni,Giorni.Dettagli,Giorni.Dettagli.Vpa"
                ).SingleOrDefaultAsync();
        }
    }    
}
