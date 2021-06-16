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
using Microsoft.Extensions.Logging;
using Ictx.WebApp.Application.UnitOfWork;
using Ictx.WebApp.Application.Repositories;

namespace Ictx.WebApp.UnitTest
{
    public class DipendenteServiceTest
    {
        private readonly DipendenteBO _sut;
        private readonly Mock<ILogger<DipendenteBO>> _logger = new();
        private readonly Mock<IAppUnitOfWork> _appUnitOfWork = new ();
        private readonly Mock<IGenericRepository<Dipendente>> _dipendenteRepository = new ();

        private readonly List<Dipendente> _listaDipendentiFake;

        public DipendenteServiceTest()
        {
            this._sut = new DipendenteBO(this._logger.Object, this._appUnitOfWork.Object);
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
            var response = await this._sut.ReadAsync(dipendente.Id);

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
            var responseResult = await this._sut.ReadAsync(0);

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
            var response = await this._sut.DeleteAsync(dipendente.Id);

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
            var responseResult = await this._sut.DeleteAsync(0);

            // Assert.
            responseResult.IsSuccess.Should().BeFalse();
            responseResult.Exception.Should().BeOfType(typeof(NotFoundException));
        }

        #region Utils

        private static List<Dipendente> GetListaDipendentiFake()
        {
            return new List<Dipendente>()
            {
                new Dipendente("PRDFRN52D67D316U", "Apreda", "Fabrina", "F", new DateTime(1952, 04, 27)),
                new Dipendente("MRTMND85P55F365E", "Martino", "Merinda", "F", new DateTime(1985, 09, 15)),
                new Dipendente("MRSSNZ92P15M159G", "Morselli", "Assenzio", "M", new DateTime(1992, 09, 15)),
                new Dipendente("BNGSRA91L12F329R", "Buongrazio", "Saro", "M", new DateTime(1991, 07, 12)),
                new Dipendente("DMLTLC87B26C330A", "D'emilia", "Italico", "M", new DateTime(1987, 02, 26)),
                new Dipendente("NFNVNT76A10A160I", "Infante", "Valento", "M", new DateTime(1976, 01, 10)),
                new Dipendente("DLGTTL67E54A761P", "Delgado", "Attala", "F", new DateTime(1967, 05, 14)),
                new Dipendente("WLLDRO68H17B361E", "Williams", "Dorio", "M", new DateTime(1968, 06, 17)),
                new Dipendente("FRRLRT94L53H437X", "Ferrari", "Laurita", "F", new DateTime(1994, 07, 13)),
                new Dipendente("BNASSM65B53B692O", "Baiano", "Sigismonda", "F", new DateTime(1965, 02, 13)),
                new Dipendente("CZZRMD99B01L601R", "Cozzolino", "Remido", "M", new DateTime(1999, 02, 01)),
                new Dipendente("BRNPMT55L41E388X", "Brunetti", "Primetta", "F", new DateTime(1955, 07, 01)),
                new Dipendente("DTTGRD79P26H938X", "Dotti", "Galardo", "M", new DateTime(1979, 09, 26)),
                new Dipendente("RZUGLN62L66G774X", "Urzo", "Gigliana", "F", new DateTime(1962, 07, 26)),
                new Dipendente("PLSRLT00H28L276N", "Pulsoni", "Risoluto", "M", new DateTime(2000, 06, 28)),
                new Dipendente("BRNFTT00S60A278W", "Bernardi", "Fifetta", "F", new DateTime(2000, 11, 20)),
                new Dipendente("BRNLND67M25L385J", "Brunetti", "Alindo", "M", new DateTime(1967, 08, 25)),
                new Dipendente("BRGFLL82M53C908P", "Baragliu", "Finella", "F", new DateTime(1982, 08, 13)),
                new Dipendente("DTRSMN69B21G499Y", "Di tuoro ", "Osmano", "M", new DateTime(1969, 02, 21)),
                new Dipendente("LCNPLE61B15F219H", "Lucani", "Euplio", "M", new DateTime(1961, 02, 15))
            };
        }

        #endregion
    }
}
