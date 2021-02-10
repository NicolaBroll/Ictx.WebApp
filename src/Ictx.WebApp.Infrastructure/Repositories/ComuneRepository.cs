using Ictx.WebApp.Core.Entities;
using Ictx.WebApp.Infrastructure.Data;

namespace Ictx.WebApp.Infrastructure.Repositories
{
    public class ComuneRepository: GenericRepository<Comune>
    {
        public ComuneRepository(AppDbContext context) : base(context)
        {

        }
    }    
}
