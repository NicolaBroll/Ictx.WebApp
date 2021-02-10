using Ictx.WebApp.Core.Entities;
using Ictx.WebApp.Infrastructure.Data;

namespace Ictx.WebApp.Infrastructure.Repositories
{
    public class ImpresaRepository : GenericRepository<Impresa>
    {
        public ImpresaRepository(AppDbContext context) : base(context)
        {

        }
    }    
}
