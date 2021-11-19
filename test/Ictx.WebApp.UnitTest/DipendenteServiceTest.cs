using Moq;
using Xunit;
using FluentAssertions;

using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using Ictx.WebApp.Core.Entities;
using Ictx.WebApp.Core.Exceptions;
using Ictx.WebApp.Application.BO;
using Ictx.WebApp.Application.Contracts.UnitOfWork;
using Ictx.WebApp.Application.Contracts.Repositories;
using Ictx.WebApp.Core.Models;
using Ictx.WebApp.Application.Validators;

namespace Ictx.WebApp.UnitTest;

public class DipendenteServiceTest
{
    private readonly DipendenteBO _sut;
    private readonly Mock<IAppUnitOfWork> _appUnitOfWork = new ();
    private readonly Mock<IGenericRepository<Dipendente>> _dipendenteRepository = new ();

    private readonly CancellationToken _cancellationToken;
    private readonly List<Dipendente> _listaDipendentiFake;

    public DipendenteServiceTest()
    {
        var validator = new DipendenteValidator();

        this._sut = new DipendenteBO(this._appUnitOfWork.Object, validator);
        this._cancellationToken = new CancellationToken();
        this._listaDipendentiFake = GetListaDipendentiFake();
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnDipendente_WhenDipendenteExists()
    {
        // Arrange.
        var dipendente = this._listaDipendentiFake.First();
        dipendente.Id = 987;

        this._dipendenteRepository.Setup(x => x.ReadAsync(dipendente.Id, new CancellationToken())).ReturnsAsync(dipendente);

        this._appUnitOfWork.Setup(x => x.DipendenteRepository).Returns(this._dipendenteRepository.Object);

        // Act.
        var response = await this._sut.ReadAsync(dipendente.Id, this._cancellationToken);

        // Assert.
        response.IsSuccess.Should().BeTrue();
        response.ResultData.Id.Should().Be(dipendente.Id);
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnNotFoundException_WhenDipendenteNotExists()
    {
        // Arrange.
        this._dipendenteRepository.Setup(x => x.ReadAsync(It.IsAny<int>(), new CancellationToken())).ReturnsAsync(() => null);
        this._appUnitOfWork.Setup(x => x.DipendenteRepository).Returns(this._dipendenteRepository.Object);

        // Act.
        var responseResult = await this._sut.ReadAsync(0, this._cancellationToken);

        // Assert.
        responseResult.IsSuccess.Should().BeFalse();
        responseResult.Exception.Should().BeOfType(typeof(NotFoundException));
    }

    [Fact]
    public async Task DeleteAsync_ShouldReturnTrue_WhenDipendenteExists()
    {
        // Arrange.
        var dipendente = this._listaDipendentiFake.First();
        dipendente.Id = 987;

        this._dipendenteRepository.Setup(x => x.ReadAsync(dipendente.Id, new CancellationToken())).ReturnsAsync(dipendente);
        this._appUnitOfWork.Setup(x => x.DipendenteRepository).Returns(this._dipendenteRepository.Object);

        // Act.
        var response = await this._sut.DeleteAsync(dipendente.Id, this._cancellationToken);

        // Assert.
        response.IsSuccess.Should().BeTrue();
        response.ResultData.Should().BeTrue();
    }

    [Fact]
    public async Task DeleteAsync_ShouldReturnNotFoundException_WhenDipendenteNotExists()
    {
        // Arrange.
        this._dipendenteRepository.Setup(x => x.ReadAsync(It.IsAny<int>(), new CancellationToken())).ReturnsAsync(() => null);
        this._appUnitOfWork.Setup(x => x.DipendenteRepository).Returns(this._dipendenteRepository.Object);

        // Act.
        var responseResult = await this._sut.DeleteAsync(0, this._cancellationToken);

        // Assert.
        responseResult.IsSuccess.Should().BeFalse();
        responseResult.Exception.Should().BeOfType(typeof(NotFoundException));
    }

    #region Utils

    private static List<Dipendente> GetListaDipendentiFake()
    {
        return new List<Dipendente>()
        {
            new Dipendente("Apreda", "Fabrina", Sesso.F, new DateTime(1952, 04, 27)),
            new Dipendente("Martino", "Merinda", Sesso.F, new DateTime(1985, 09, 15)),
            new Dipendente("Morselli", "Assenzio", Sesso.M, new DateTime(1992, 09, 15)),
            new Dipendente("Buongrazio", "Saro", Sesso.M, new DateTime(1991, 07, 12)),
            new Dipendente("D'emilia", "Italico", Sesso.M, new DateTime(1987, 02, 26)),
            new Dipendente("Infante", "Valento", Sesso.M, new DateTime(1976, 01, 10)),
            new Dipendente("Delgado", "Attala", Sesso.F, new DateTime(1967, 05, 14)),
            new Dipendente("Williams", "Dorio", Sesso.M, new DateTime(1968, 06, 17)),
            new Dipendente("Ferrari", "Laurita", Sesso.F, new DateTime(1994, 07, 13)),
            new Dipendente("Baiano", "Sigismonda", Sesso.F, new DateTime(1965, 02, 13)),
            new Dipendente("Cozzolino", "Remido", Sesso.M, new DateTime(1999, 02, 01)),
            new Dipendente("Brunetti", "Primetta", Sesso.F, new DateTime(1955, 07, 01)),
            new Dipendente("Dotti", "Galardo", Sesso.M, new DateTime(1979, 09, 26)),
            new Dipendente("Urzo", "Gigliana", Sesso.F, new DateTime(1962, 07, 26)),
            new Dipendente("Pulsoni", "Risoluto", Sesso.M, new DateTime(2000, 06, 28)),
            new Dipendente("Bernardi", "Fifetta", Sesso.F, new DateTime(2000, 11, 20)),
            new Dipendente("Brunetti", "Alindo", Sesso.M, new DateTime(1967, 08, 25)),
            new Dipendente("Baragliu", "Finella", Sesso.F, new DateTime(1982, 08, 13)),
            new Dipendente("Di tuoro ", "Osmano", Sesso.M, new DateTime(1969, 02, 21)),
            new Dipendente("Lucani", "Euplio", Sesso.M, new DateTime(1961, 02, 15))
        };
    }

    #endregion
}