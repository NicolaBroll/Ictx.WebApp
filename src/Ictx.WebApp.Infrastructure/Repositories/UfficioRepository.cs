using Ictx.WebApp.Core.Entities;
using Ictx.WebApp.Infrastructure.Data;

namespace Ictx.WebApp.Infrastructure.Repositories
{
    public class UfficioRepository : GenericRepository<Ufficio>
    {
        public UfficioRepository(AppDbContext context) : base(context)
        {

        }
    }    
}
