using System.Threading.Tasks;
using LanguageExt.Common;
using Ictx.WebApp.Core.Entities;
using Ictx.WebApp.Core.Exceptions.Dipendente;
using Ictx.WebApp.Infrastructure.UnitOfWork;
using Ictx.WebApp.Infrastructure.Services.Interfaces;

namespace Ictx.WebApp.Infrastructure.Services
{
    public class DittaService : IDittaService
    {
        private readonly IAppUnitOfWork _appUnitOfWork;

        public DittaService(IAppUnitOfWork appUnitOfWork)
        {
            this._appUnitOfWork = appUnitOfWork;
        }

        public async Task<Result<Ditta>> GetByIdAsync(int id)
        {
            var dipendente = await this._appUnitOfWork.DittaRepository.ReadAsync(id);

            if (dipendente is null)
                return new Result<Ditta>(new NotFoundException($"Ditta con id: {id} non trovata."));

            return dipendente;
        }
    }
}
