using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Bogus;
using Ictx.WebApp.Application.BO;
using Ictx.WebApp.Core.Entities;
using Ictx.WebApp.Core.Models;

namespace Ictx.WebApp.Application.Data;

public class FakeDataGenerator
{
    private readonly ILogger<FakeDataGenerator> _logger;
    private readonly DipendenteBO               _dipendenteBO;
    private readonly CancellationToken          _cancellationToken;

    public FakeDataGenerator(ILogger<FakeDataGenerator> logger, DipendenteBO dipendenteBO)
    {
        this._logger            = logger;
        this._dipendenteBO      = dipendenteBO;
        this._cancellationToken = new CancellationToken();
    }

    public async Task Genera()
    {
        await GenerDipendenti();
    }

    private async Task GenerDipendenti()
    {
        var lstDipendenti = await this._dipendenteBO.ReadManyPaginatedAsync(new Models.PaginationModel
        {
            Page = 1,
            PageSize = 1
        }, this._cancellationToken);

        if (!lstDipendenti.Data.Any())
        {
            this._logger.LogInformation("Seeding dipendenti...");

            var dipendenteFake = new Faker<Dipendente>()
                .RuleFor(x => x.Cognome, f => f.Person.LastName)
                .RuleFor(x => x.Nome, f => f.Person.FirstName)
                .RuleFor(x => x.Sesso, f => f.Person.ToSesso())
                .RuleFor(x => x.DataNascita, f => f.Person.DateOfBirth);

            await this._dipendenteBO.InsertManyAsync(dipendenteFake.Generate(3000), this._cancellationToken);

            this._logger.LogInformation("OK dipendenti seeded.");
        }
    }
}

public static class BogusExtensions
{
    public static Sesso ToSesso(this Person person)
    {
        if (person.Gender == Bogus.DataSets.Name.Gender.Male)
        {
            return Sesso.M;
        }

        return Sesso.F;
    }
}
