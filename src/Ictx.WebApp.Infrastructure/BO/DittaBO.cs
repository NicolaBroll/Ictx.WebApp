using System.Threading.Tasks;
using Ictx.WebApp.Core.Entities;
using Ictx.WebApp.Infrastructure.UnitOfWork;
using Ictx.WebApp.Core.Exceptions.Dipendente;
using Ictx.WebApp.Infrastructure.BO.Interfaces;
using Ictx.Framework.BO;
using Ictx.Framework.Models;

namespace Ictx.WebApp.Infrastructure.BO
{
    public class DittaBO : BaseBO<Ditta, int, ServiceParameters>, IDittaBO
    {
        private readonly IAppUnitOfWork     _appUnitOfWork;

        public DittaBO(IAppUnitOfWork appUnitOfWork)
        {
            this._appUnitOfWork = appUnitOfWork;
        }


        /// <summary>
        /// Ritorna una ditta. Se non viene trovata, ritorna NotFoundException.
        /// </summary>
        /// <param name="key">Id dipendente</param>
        /// <returns>Ritorna un BOResult<Ditta> contenente la ditta associata all'id richiesto oppure una 
        /// NotFoundException nel caso la ditta non sia presente. </returns>
        protected override async Task<BOResult<Ditta>> ReadViewAsync(int key)
        {
            var ditta = await this._appUnitOfWork.DittaRepository.ReadAsync(key);

            if (ditta is null)
                return new BOResult<Ditta>(new NotFoundException($"Ditta con id: {key} non trovata."));

            return new BOResult<Ditta>(ditta);
        }      
    }
}
