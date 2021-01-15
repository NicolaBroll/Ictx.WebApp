using Ictx.WebApp.Core.Entities;
using Ictx.WebApp.Infrastructure.Data;

namespace Ictx.WebApp.Infrastructure.Repositories
{
    public class DipendenteRepository: GenericRepository<Dipendente>
    {
        public DipendenteRepository(AppDbContext context) : base(context)
        {

        }
    }    
}
