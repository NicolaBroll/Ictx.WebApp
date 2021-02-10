using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Ictx.WebApp.Core.Entities;
using Microsoft.EntityFrameworkCore;
using static Ictx.WebApp.Core.Models.DipendenteModel;

namespace Ictx.WebApp.Infrastructure.Data
{
    public class SeedDatabase
    {
        private readonly AppDbContext _context;
        private readonly string _seedDataDirectory;
        private readonly Random _random;

        public SeedDatabase(AppDbContext context)
        {
            this._context = context;
            this._seedDataDirectory = Path.Combine(Directory.GetCurrentDirectory(), "SeedData");
            this._random = new Random();
        }

        public async void Initialize()
        {
            _context.Database.EnsureCreated();

            // Uffici base.
            if(!_context.UfficioBase.Any())
            {
                await PopolaUfficiBase();
                await PopolaUffici();
                await PopolaImprese();
                await PopolaDitte();
                await PopolaDipendenti();
            }

            _context.Dispose();
        }

        private async Task PopolaDitte()
        {
            await _context.Ditta.AddRangeAsync(await GetDitte());
            await _context.SaveChangesAsync(); 
        }

        private async Task<List<Ditta>> GetDitte()
        {
            var result = new List<Ditta>();
            var lstImprese = await this._context.Impresa.ToListAsync();

            foreach(var impresa in lstImprese)
            {
                foreach(var nRandom in Enumerable.Range(1, this._random.Next(1, 5)))
                {
                    result.Add(new Ditta(nRandom, $"Ditta {nRandom}", impresa));
                }
            }

            return result;    
        }

        private async Task PopolaImprese()
        {
            await _context.Impresa.AddRangeAsync(await GetImprese());
            await _context.SaveChangesAsync(); 
        }

        private async Task<List<Impresa>> GetImprese()
        {
            var result = new List<Impresa>();
            var lstUffici = await this._context.Ufficio.ToListAsync();

            foreach(var ufficio in lstUffici)
            {
                foreach(var nRandom in Enumerable.Range(1, this._random.Next(1, 100)))
                {
                    result.Add(new Impresa(nRandom, $"Impresa {nRandom}", ufficio));
                }
            }

            return result;
        }

        private async Task PopolaUffici()
        {
            await _context.Ufficio.AddRangeAsync(await GetUffici());
            await _context.SaveChangesAsync();   
         }

        private async Task<List<Ufficio>> GetUffici()
        {
            var result = new List<Ufficio>();
            var lstUfficiBase = await this._context.UfficioBase.ToListAsync();

            foreach(var ufficioBase in lstUfficiBase)
            {
                foreach(var nRandom in Enumerable.Range(1, this._random.Next(1, 15)))
                {
                    result.Add(new Ufficio(nRandom, $"Sottoufficio {nRandom} - {ufficioBase.Denominazione}", ufficioBase));
                }
            }

            return result;
        }

        private async Task PopolaUfficiBase()
        {
            await _context.UfficioBase.AddRangeAsync(GetUfficiBase());
            await _context.SaveChangesAsync();
        }

        private List<UfficioBase> GetUfficiBase()
        {
            return new List<UfficioBase>()
            {
                new UfficioBase(1, "Ufficio base Trento"),
                new UfficioBase(2, "Ufficio base Bolzano"),
                new UfficioBase(3, "Ufficio base Verona")
            };
        }

        public async Task PopolaDipendenti()
        {
            await _context.Dipendente.AddRangeAsync(await GetDipendenti());
            await _context.SaveChangesAsync();
        }

        private async Task<List<Dipendente>> GetDipendenti()
        {
            var result = new List<Dipendente>();
            var lstDitte = await this._context.Ditta.ToListAsync();
            var lstNomi = File.ReadAllLines(Path.Combine(this._seedDataDirectory, "Nomi.txt"));
            var lstCogomi = File.ReadAllLines(Path.Combine(this._seedDataDirectory, "Cognomi.txt"));
            var countlstCogomi = lstCogomi.Count();

            foreach(var ditta in lstDitte)
            {
                foreach(var nome in lstNomi)
                {
                    var cognome = lstCogomi[this._random.Next(1, countlstCogomi)];
                    var dataDinascita = new DateTime(); // TODO
                    var comuneNascita = ""; // TODO
                    var sesso = (Sesso)this._random.Next(0, 1);
                    var codiceFiscale = Core.Utils.CodiceFiscale.Calcola(nome, cognome, dataDinascita, sesso.ToString(), comuneNascita);

                    result.Add(new Dipendente(codiceFiscale, cognome, nome, sesso, dataDinascita, ditta));
                }
            }

            return result;
        }
    }
}
