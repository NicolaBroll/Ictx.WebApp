using System.Threading;
using System.Threading.Tasks;
using Bogus;
using Ictx.WebApp.Application.BO;
using Ictx.WebApp.Core.Entities;
using Ictx.WebApp.Core.Models;

namespace Ictx.WebApp.Application.Data;

public class FakeDataGenerator
{
    private readonly DipendenteBO _dipendenteBO;
    private readonly CancellationToken _cancellationToken;

    public FakeDataGenerator(DipendenteBO dipendenteBO)
    {
        this._dipendenteBO = dipendenteBO;
        this._cancellationToken = new System.Threading.CancellationToken();
    }

    public async Task Genera()
    {
        await GenerDipendenti();
    }

    private async Task GenerDipendenti()
    {
        var dipendenteFake = new Faker<Dipendente>()
            .RuleFor(x => x.Cognome, f => f.Person.LastName)
            .RuleFor(x => x.Nome, f => f.Person.FirstName)
            .RuleFor(x => x.Sesso, f => f.Person.ToSesso())
            .RuleFor(x => x.DataNascita, f => f.Person.DateOfBirth);

        await this._dipendenteBO.InsertManyAsync(dipendenteFake.Generate(100), this._cancellationToken);
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
