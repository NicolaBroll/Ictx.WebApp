using Ictx.WebApp.Application.Repositories;
using Ictx.WebApp.Core.Entities;
using Ictx.WebApp.Infrastructure.Data.App;

namespace Ictx.WebApp.Infrastructure.Repositories
{
    public class DipendenteRepository : GenericRepository<Dipendente, AppDbContext>, IDipendenteRepository
    {
        private readonly AppDbContext _appDbContext;

        public DipendenteRepository(AppDbContext appDbContext) : base(appDbContext)
        {
            _appDbContext = appDbContext;
        }
    }
}
