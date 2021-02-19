using System.Threading.Tasks;
using Ictx.WebApp.Core.Entities;
using Ictx.WebApp.Core.Exceptions.Dipendente;
using Ictx.WebApp.Infrastructure.UnitOfWork;

namespace Ictx.WebApp.Infrastructure.Services
{
    public class DittaService
    {
        private readonly AppUnitOfWork _appUnitOfWork;

        public DittaService(AppUnitOfWork appUnitOfWork)
        {
            this._appUnitOfWork = appUnitOfWork;
        }

        public async Task<Ditta> GetByIdAsync(int id)
        {
            var dipendente = await this._appUnitOfWork.DittaRepository.ReadAsync(id);

            if (dipendente is null)
                throw new DittaNotFoundException($"Ditta con id: {id} non trovata.");

            return dipendente;
        }
    }
}
