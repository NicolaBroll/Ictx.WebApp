using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Ictx.WebApp.Core.Entities;
using Ictx.WebApp.Infrastructure.Services.Interface;
using Ictx.WebApp.Infrastructure.UnitOfWork;

namespace Ictx.WebApp.Infrastructure.Services.Implementation
{
    public class DipendenteService: IDipendenteService
    {
        private readonly AppUnitOfWork _appUnitOfWork;

        public DipendenteService(AppUnitOfWork appUnitOfWork)
        {
            this._appUnitOfWork = appUnitOfWork;
        }

        public async Task<IEnumerable<Dipendente>> GetListAsync()
        {
            return await this._appUnitOfWork.DipendenteRepository.QueryMany().ToListAsync();
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
