using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Ictx.WebApp.Core.Entities;
using Ictx.WebApp.Infrastructure.Services.Interface;
using Ictx.WebApp.Infrastructure.UnitOfWork;
using static Ictx.WebApp.Core.Models.PaginationModel;

namespace Ictx.WebApp.Infrastructure.Services.Implementation
{
    public class DipendenteService: IDipendenteService
    {
        private readonly AppUnitOfWork _appUnitOfWork;

        public DipendenteService(AppUnitOfWork appUnitOfWork)
        {
            this._appUnitOfWork = appUnitOfWork;
        }

        public async Task<PageResult<Dipendente>> GetListAsync(PaginationFilterModel paginationFilterModel)
        {
            var qy = this._appUnitOfWork.DipendenteRepository.QueryMany();

            var count = qy.Count();
            var list = await qy.Skip((paginationFilterModel.Page - 1) * paginationFilterModel.PageSize).Take(paginationFilterModel.PageSize).ToListAsync();

            return new PageResult<Dipendente>(list, count);
        }

        public async Task<Dipendente> GetByIdAsync(int id)
        {
            return await this._appUnitOfWork.DipendenteRepository.ReadAsync(id);
        }

        public async Task<Dipendente> InsertAsync(Dipendente dipendente)
        {
            dipendente.Inserted = DateTime.UtcNow;
            dipendente.Updated = DateTime.UtcNow;
            dipendente.CodiceFiscale = dipendente.CodiceFiscale.ToUpper();
            dipendente.Nome = dipendente.Nome.ToUpper();
            dipendente.Cognome = dipendente.Cognome.ToUpper();

            await this._appUnitOfWork.DipendenteRepository.InsertAsync(dipendente);
            await this._appUnitOfWork.Save();

            return dipendente;
        }

        public async Task<Dipendente> SaveAsync(int id, Dipendente dipendente)
        {
            dipendente.Updated = DateTime.UtcNow;
            dipendente.CodiceFiscale = dipendente.CodiceFiscale.ToUpper();
            dipendente.Nome = dipendente.Nome.ToUpper();
            dipendente.Cognome = dipendente.Cognome.ToUpper();

            await this._appUnitOfWork.Save();
            return dipendente;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                this._appUnitOfWork.DipendenteRepository.Delete(id);
                await this._appUnitOfWork.Save();

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
