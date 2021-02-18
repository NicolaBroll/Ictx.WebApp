using Ictx.WebApp.Core.Entities;
using Ictx.WebApp.Infrastructure.Data;

namespace Ictx.WebApp.Infrastructure.Repositories
{
    public class DittaRepository : GenericRepository<Ditta>
    {
        public DittaRepository(AppDbContext context) : base(context)
        {

        }
    }    
}
