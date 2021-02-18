using Ictx.WebApp.Core.Entities;
using Ictx.WebApp.Infrastructure.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using static Ictx.WebApp.Core.Models.DipendenteModel;

namespace Ictx.WebApp.Api.Database.SeedData
{
    public class SeedDipendente : SeedDataCore
    {
        public SeedDipendente(AppUnitOfWork appUnitOfWork) : base(appUnitOfWork)
        { }

        public override async Task Popola()
        {
            await this._appUnitOfWork.DipendenteRepository.InsertManyAsync(await GetDipendenti());
            await this._appUnitOfWork.SaveAsync();
        }

        private async Task<List<Dipendente>> GetDipendenti()
        {
            var result = new List<Dipendente>();
            var lstDitte = await this._appUnitOfWork.DittaRepository.ReadManyAsync();
            var lstNomi = File.ReadAllLines(Path.Combine(this._seedStaticDataDirectory, "Nomi.txt")).Where(x => x.Length > 3).ToArray();
            var lstCogomi = File.ReadAllLines(Path.Combine(this._seedStaticDataDirectory, "Cognomi.txt")).Where(x => x.Length > 3).ToArray();
            var lstComuni = await this._appUnitOfWork.ComuneRepository.ReadManyAsync();
            var arrayComuni = lstComuni.ToArray();

            var countlstComuni = arrayComuni.Count();
            var countlstCogomi = lstCogomi.Count();

            foreach (var ditta in lstDitte)
            {
                var countDipendentiDaInserire = this._random.Next(5, 200);

                foreach (var nome in lstNomi.OrderBy(x => this._random.Next()).Take(countDipendentiDaInserire).ToArray())
                {
                    var cognome = lstCogomi[this._random.Next(1, countlstCogomi)];
                    var dataDinascita = GetRandomDate();
                    var comuneNascita = arrayComuni[this._random.Next(1, countlstComuni)];
                    var sesso = nome.Substring(nome.Length - 1) == "a" ? Sesso.F : Sesso.M;
                    var codiceFiscale  = Core.Utils.CodiceFiscale.Calcola(nome, cognome, dataDinascita, sesso.ToString(), comuneNascita.Codice);

                    result.Add(new Dipendente(codiceFiscale, cognome, nome, sesso, dataDinascita, ditta));
                }
            }

            return result;
        }

        private DateTime GetRandomDate()
        {
            var start = new DateTime(1950, 1, 1);
            var end = new DateTime(2002, 1, 1);
            var range = (end - start).Days;

            return start.AddDays(this._random.Next(range));
        }
    }
}
