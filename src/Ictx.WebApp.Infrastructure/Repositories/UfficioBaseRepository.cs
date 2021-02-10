using Ictx.WebApp.Core.Entities;
using Ictx.WebApp.Infrastructure.Data;

namespace Ictx.WebApp.Infrastructure.Repositories
{
    public class UfficioBaseRepository : GenericRepository<UfficioBase>
    {
        public UfficioBaseRepository(AppDbContext context) : base(context)
        {

        }
    }    
}
