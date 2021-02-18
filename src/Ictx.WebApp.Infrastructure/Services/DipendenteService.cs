using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Ictx.WebApp.Core.Entities;
using Ictx.WebApp.Core.Exceptions.Dipendente;
using Ictx.WebApp.Infrastructure.UnitOfWork;
using static Ictx.WebApp.Core.Models.PaginationModel;
using static Ictx.WebApp.Core.Models.DipendenteModel;

namespace Ictx.WebApp.Infrastructure.Services
{
    public class DipendenteService
    {
        private readonly AppUnitOfWork _appUnitOfWork;

        public DipendenteService(AppUnitOfWork appUnitOfWork)
        {
            this._appUnitOfWork = appUnitOfWork;
        }

        public async Task<PageResult<Dipendente>> GetListAsync(DipendenteListFilter filter)
        {
            var qy = this._appUnitOfWork.DipendenteRepository.QueryMany(
                filter: x => x.DittaId == filter.DittaId,
                orderBy: x => x.OrderBy(o => o.Cognome).ThenBy(x => x.Nome)
                );

            var count = qy.Count();
            var list = await qy.Skip((filter.Page - 1) * filter.PageSize).Take(filter.PageSize).ToListAsync();

            return new PageResult<Dipendente>(list, count);
        }

        public async Task<Dipendente> GetByIdAsync(int id)
        {
            var dipendente = await this._appUnitOfWork.DipendenteRepository.ReadAsync(id);

            if (dipendente is null)
                throw new DipendenteNotFoundException($"Dipendente con id: {id} non trovato.");

            return dipendente;
        }

        public async Task<Dipendente> InsertAsync(Dipendente model)
        {
            var ditta = await this._appUnitOfWork.DittaRepository.ReadAsync(model.DittaId);

            if (ditta is null)
                throw new DittaNotFoundException($"Ditta con id: {model.DittaId} non trovata.");

            var objToInsert = new Dipendente(model.CodiceFiscale, model.Cognome, model.Nome, model.Sesso, model.DataNascita, ditta);

            await this._appUnitOfWork.DipendenteRepository.InsertAsync(objToInsert);
            await this._appUnitOfWork.SaveAsync();

            return objToInsert;
        }

        public async Task<Dipendente> SaveAsync(int id, Dipendente model)
        {
            var objToUpdate = await this._appUnitOfWork.DipendenteRepository.ReadAsync(id);

            if (objToUpdate is null)
                throw new DipendenteNotFoundException($"Dipendente con id: {id} non trovato.");

            objToUpdate.Updated = DateTime.UtcNow;
            objToUpdate.CodiceFiscale = model.CodiceFiscale.ToUpper();
            objToUpdate.Nome = Char.ToUpperInvariant(model.Nome[0]) + model.Nome.ToLower().Substring(1);
            objToUpdate.Cognome = Char.ToUpperInvariant(model.Cognome[0]) + model.Cognome.ToLower().Substring(1);
            objToUpdate.Sesso = model.Sesso;
            objToUpdate.DataNascita = model.DataNascita;

            this._appUnitOfWork.DipendenteRepository.Update(objToUpdate);
            await this._appUnitOfWork.SaveAsync();

            return objToUpdate;
        }

        public async Task DeleteAsync(int id)
        {
            var objToDelete = await this._appUnitOfWork.DipendenteRepository.ReadAsync(id);

            if(objToDelete is null)
                throw new DipendenteNotFoundException($"Dipendente con id: {id} non trovato.");

            this._appUnitOfWork.DipendenteRepository.Delete(id);
            await this._appUnitOfWork.SaveAsync();
        }
    }
}
