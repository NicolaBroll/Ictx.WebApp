using Ictx.WebApp.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Ictx.WebApp.Core.Models.DipendenteModel;

namespace Ictx.WebApp.Infrastructure.Data
{
    public class SeedDatabase
    {
        private readonly AppDbContext _context;

        public SeedDatabase(AppDbContext context)
        {
            _context = context;
        }

        public async void Initialize()
        {
            _context.Database.EnsureCreated();

            // Dipendente.
            if (!_context.Dipendente.Any())
                await PopolaDipendenti();

            // FoglioPresenzaVpa.
            if (!_context.FoglioPresenzaVpa.Any())
                await PopolaFoglioPresenzaVpa();

            _context.Dispose();
        }

        #region Dipendente

        public async Task PopolaDipendenti()
        {
            await _context.Dipendente.AddRangeAsync(GetDipendenti());
            await _context.SaveChangesAsync();
        }

        private List<Dipendente> GetDipendenti()
        {
            return new List<Dipendente>()
            {
                new Dipendente()
                {
                    CodiceFiscale = "RSSMRA80M01H501L",
                    Cognome = "Rossi",
                    Nome = "Mario",
                    Sesso = Sesso.M,
                    DataNascita = new DateTime(1980, 8, 1),
                    Inserted = DateTime.UtcNow,
                    Updated = DateTime.UtcNow
                },
                new Dipendente()
                {
                    CodiceFiscale = "BNCLCU95C12F205K",
                    Cognome = "Bianchi",
                    Nome = "Luca",
                    Sesso = Sesso.M,
                    DataNascita = new DateTime(1995, 3, 12),
                    Inserted = DateTime.UtcNow,
                    Updated = DateTime.UtcNow
                },
                new Dipendente()
                {
                    CodiceFiscale = "BRLNCL94E25L378N",
                    Cognome = "Nicola",
                    Nome = "Broll",
                    Sesso = Sesso.M,
                    DataNascita = new DateTime(1994, 5, 25),
                    Inserted = DateTime.UtcNow,
                    Updated = DateTime.UtcNow
                },
                new Dipendente()
                {
                    CodiceFiscale = "SNTLCU00T53G273X",
                    Cognome = "Santa",
                    Nome = "Lucia",
                    Sesso = Sesso.F,
                    DataNascita = new DateTime(2000, 12, 13),
                    Inserted = DateTime.UtcNow,
                    Updated = DateTime.UtcNow
                },
            };
        }

        #endregion

        #region FoglioPresenzaVpa

        public async Task PopolaFoglioPresenzaVpa()
        {
            await _context.FoglioPresenzaVpa.AddRangeAsync(GetFoglioPresenzaVpa());
            await _context.SaveChangesAsync();
        }

        private List<FoglioPresenzaVpa> GetFoglioPresenzaVpa()
        {
            return new List<FoglioPresenzaVpa>()
            {
                new FoglioPresenzaVpa("OR", 1, "Orario ordinario"),
                new FoglioPresenzaVpa("FE", 2, "Ferie"),
                new FoglioPresenzaVpa("PR", 3, "Permesso"),
                new FoglioPresenzaVpa("ML", 4, "Malattia")

            };
        }

        #endregion
    }
}
