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

        public async Task<PageResult<Dipendente>> GetListAsync(PaginationFilterModel paginationFilterModel, int dittaId)
        {
            var qy = this._appUnitOfWork.DipendenteRepository.QueryMany(
                filter: x => x.DittaId == dittaId,
                orderBy: x => x.OrderBy(o => o.Cognome).ThenBy(x => x.Nome)
                );

            var count = qy.Count();
            var list = await qy.Skip((paginationFilterModel.Page - 1) * paginationFilterModel.PageSize).Take(paginationFilterModel.PageSize).ToListAsync();

            return new PageResult<Dipendente>(list, count);
        }

        public async Task<Dipendente> GetByIdAsync(int id)
        {
            return await this._appUnitOfWork.DipendenteRepository.ReadAsync(id);
        }

        public async Task<Dipendente> InsertAsync(Dipendente model)
        {
            var ditta = await this._appUnitOfWork.DittaRepository.ReadAsync(model.DittaId);
            var objToInsert = new Dipendente(model.CodiceFiscale, model.Cognome, model.Nome, model.Sesso, model.DataNascita, ditta);

            await this._appUnitOfWork.DipendenteRepository.InsertAsync(objToInsert);
            await this._appUnitOfWork.SaveAsync();

            return model;
        }

        public async Task<Dipendente> SaveAsync(int id, Dipendente model)
        {
            var objToUpdate = await this._appUnitOfWork.DipendenteRepository.ReadAsync(id);

            objToUpdate.Updated = DateTime.UtcNow;
            objToUpdate.CodiceFiscale = model.CodiceFiscale.ToUpper();
            objToUpdate.Nome = Char.ToUpperInvariant(model.Nome[0]) + model.Nome.ToLower().Substring(1);
            objToUpdate.Cognome = Char.ToUpperInvariant(model.Cognome[0]) + model.Cognome.ToLower().Substring(1);
            objToUpdate.DataNascita = model.DataNascita;

            this._appUnitOfWork.DipendenteRepository.Update(objToUpdate);
            await this._appUnitOfWork.SaveAsync();

            return model;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            this._appUnitOfWork.DipendenteRepository.Delete(id);
            await this._appUnitOfWork.SaveAsync();

            return true; 
        }
    }
}
