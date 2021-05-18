using Moq;
using Xunit;
using FluentAssertions;

using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Ictx.WebApp.Core.Entities;
using Ictx.WebApp.Core.Models;
using Ictx.WebApp.Core.Exceptions.Dipendente;
using Ictx.WebApp.Application.BO;
using Microsoft.Extensions.Logging;
using Ictx.WebApp.Core.Interfaces;

namespace Ictx.WebApp.UnitTest
{
    public class DipendenteServiceTest
    {
        private readonly DipendenteBO _sut;
        private readonly Mock<ILogger<DipendenteBO>> _logger = new();
        private readonly Mock<ISessionData> _sessionData = new();
        private readonly Mock<IRazorViewToStringRenderer> _razorViewToStringRenderer = new();
        private readonly Mock<IMailService> _mailService = new();        
        private readonly Mock<IAppUnitOfWork> _appUnitOfWork = new ();
        private readonly Mock<IGenericRepository<Dipendente>> _dipendenteRepository = new ();

        private readonly List<Dipendente> _listaDipendentiFake;

        public DipendenteServiceTest()
        {
            this._sut = new DipendenteBO(this._logger.Object, this._razorViewToStringRenderer.Object, this._appUnitOfWork.Object, this._mailService.Object, this._sessionData.Object);
            this._listaDipendentiFake = GetListaDipendentiFake();
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnDipendente_WhenDipendenteExists()
        {
            // Arrange.
            var dipendente = this._listaDipendentiFake.First();
            dipendente.Id = 987;

            this._appUnitOfWork.Setup(x => x.DipendenteRepository).Returns(this._dipendenteRepository.Object);
            this._appUnitOfWork.Setup(x => x.DipendenteRepository.ReadAsync(dipendente.Id)).ReturnsAsync(dipendente);
            this._razorViewToStringRenderer.Setup(x => x.RenderViewToStringAsync<DipendenteEmailTemplate>(It.IsAny<string>(), It.IsAny<DipendenteEmailTemplate>())).ReturnsAsync(() => String.Empty);

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
            this._appUnitOfWork.Setup(x => x.DipendenteRepository).Returns(this._dipendenteRepository.Object);
            this._appUnitOfWork.Setup(x => x.DipendenteRepository.ReadAsync(It.IsAny<int>())).ReturnsAsync(() => null);

            // Act.
            var responseResult = await this._sut.ReadAsync(0);

            // Assert.
            responseResult.IsSuccess.Should().BeFalse();
            responseResult.Exception.Should().BeOfType(typeof(NotFoundException));
        }

        #region Utils

        private static List<Dipendente> GetListaDipendentiFake()
        {
            return new List<Dipendente>()
            {
                new Dipendente("PRDFRN52D67D316U", "Apreda", "Fabrina", Sesso.F, new DateTime(1952, 04, 27)),
                new Dipendente("MRTMND85P55F365E", "Martino", "Merinda", Sesso.F, new DateTime(1985, 09, 15)),
                new Dipendente("MRSSNZ92P15M159G", "Morselli", "Assenzio", Sesso.M, new DateTime(1992, 09, 15)),
                new Dipendente("BNGSRA91L12F329R", "Buongrazio", "Saro", Sesso.M, new DateTime(1991, 07, 12)),
                new Dipendente("DMLTLC87B26C330A", "D'emilia", "Italico", Sesso.M, new DateTime(1987, 02, 26)),
                new Dipendente("NFNVNT76A10A160I", "Infante", "Valento", Sesso.M, new DateTime(1976, 01, 10)),
                new Dipendente("DLGTTL67E54A761P", "Delgado", "Attala", Sesso.F, new DateTime(1967, 05, 14)),
                new Dipendente("WLLDRO68H17B361E", "Williams", "Dorio", Sesso.M, new DateTime(1968, 06, 17)),
                new Dipendente("FRRLRT94L53H437X", "Ferrari", "Laurita", Sesso.F, new DateTime(1994, 07, 13)),
                new Dipendente("BNASSM65B53B692O", "Baiano", "Sigismonda", Sesso.F, new DateTime(1965, 02, 13)),
                new Dipendente("CZZRMD99B01L601R", "Cozzolino", "Remido", Sesso.M, new DateTime(1999, 02, 01)),
                new Dipendente("BRNPMT55L41E388X", "Brunetti", "Primetta", Sesso.F, new DateTime(1955, 07, 01)),
                new Dipendente("DTTGRD79P26H938X", "Dotti", "Galardo", Sesso.M, new DateTime(1979, 09, 26)),
                new Dipendente("RZUGLN62L66G774X", "Urzo", "Gigliana", Sesso.F, new DateTime(1962, 07, 26)),
                new Dipendente("PLSRLT00H28L276N", "Pulsoni", "Risoluto", Sesso.M, new DateTime(2000, 06, 28)),
                new Dipendente("BRNFTT00S60A278W", "Bernardi", "Fifetta", Sesso.F, new DateTime(2000, 11, 20)),
                new Dipendente("BRNLND67M25L385J", "Brunetti", "Alindo", Sesso.M, new DateTime(1967, 08, 25)),
                new Dipendente("BRGFLL82M53C908P", "Baragliu", "Finella", Sesso.F, new DateTime(1982, 08, 13)),
                new Dipendente("DTRSMN69B21G499Y", "Di tuoro ", "Osmano", Sesso.M, new DateTime(1969, 02, 21)),
                new Dipendente("LCNPLE61B15F219H", "Lucani", "Euplio", Sesso.M, new DateTime(1961, 02, 15))
            };
        }

        #endregion
    }
}