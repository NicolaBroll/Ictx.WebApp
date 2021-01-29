using Ictx.WebApp.Core.Entities;
using Ictx.WebApp.Infrastructure.Data;

namespace Ictx.WebApp.Infrastructure.Repositories
{
    public class FoglioPresenzaDettaglioGiornoRepository : GenericRepository<FoglioPresenzaGiornoDettaglio>
    {
        public FoglioPresenzaDettaglioGiornoRepository(AppDbContext context) : base(context)
        { }

       
    }    
}
