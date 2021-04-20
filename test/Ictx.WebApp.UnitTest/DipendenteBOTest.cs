using Moq;
using Xunit;
using FluentAssertions;

using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Ictx.WebApp.Infrastructure.Services.Interfaces;
using Ictx.WebApp.Infrastructure.UnitOfWork;
using Ictx.WebApp.Infrastructure.BO;
using Ictx.WebApp.Infrastructure.BO.Interfaces;
using Ictx.WebApp.Core.Entities;
using Ictx.WebApp.Core.Models;
using Ictx.WebApp.Core.Exceptions.Dipendente;
using Ictx.Framework.Repository.Interfaces;

namespace Ictx.WebApp.UnitTest
{
    public class DipendenteBOTest
    {
        private readonly DipendenteBO _sut;
        private readonly Mock<IAppUnitOfWork> _appUnitOfWork = new ();
        private readonly Mock<IGenericRepository<Dipendente>> _dipendenteRepository = new ();
        private readonly Mock<IDateTimeService> _dateTimeService = new ();
        private readonly Mock<IDittaBO> _dittaBO = new ();

        private readonly int _dittaId;
        private readonly IReadOnlyList<Dipendente> _listaDipendentiFake;

        public DipendenteBOTest()
        {
            this._sut = new DipendenteBO(this._appUnitOfWork.Object, this._dateTimeService.Object, this._dittaBO.Object);
            this._dittaId = 1;
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

        private List<Dipendente> GetListaDipendentiFake()
        {
            return new List<Dipendente>()
            {
                new Dipendente(this._dittaId, "PRDFRN52D67D316U", "Apreda", "Fabrina", Sesso.F, new DateTime(1952, 04, 27)),
                new Dipendente(this._dittaId, "MRTMND85P55F365E", "Martino", "Merinda", Sesso.F, new DateTime(1985, 09, 15)),
                new Dipendente(this._dittaId, "MRSSNZ92P15M159G", "Morselli", "Assenzio", Sesso.M, new DateTime(1992, 09, 15)),
                new Dipendente(this._dittaId, "BNGSRA91L12F329R", "Buongrazio", "Saro", Sesso.M, new DateTime(1991, 07, 12)),
                new Dipendente(this._dittaId, "DMLTLC87B26C330A", "D'emilia", "Italico", Sesso.M, new DateTime(1987, 02, 26)),
                new Dipendente(this._dittaId, "NFNVNT76A10A160I", "Infante", "Valento", Sesso.M, new DateTime(1976, 01, 10)),
                new Dipendente(this._dittaId, "DLGTTL67E54A761P", "Delgado", "Attala", Sesso.F, new DateTime(1967, 05, 14)),
                new Dipendente(this._dittaId, "WLLDRO68H17B361E", "Williams", "Dorio", Sesso.M, new DateTime(1968, 06, 17)),
                new Dipendente(this._dittaId, "FRRLRT94L53H437X", "Ferrari", "Laurita", Sesso.F, new DateTime(1994, 07, 13)),
                new Dipendente(this._dittaId, "BNASSM65B53B692O", "Baiano", "Sigismonda", Sesso.F, new DateTime(1965, 02, 13)),
                new Dipendente(this._dittaId, "CZZRMD99B01L601R", "Cozzolino", "Remido", Sesso.M, new DateTime(1999, 02, 01)),
                new Dipendente(this._dittaId, "BRNPMT55L41E388X", "Brunetti", "Primetta", Sesso.F, new DateTime(1955, 07, 01)),
                new Dipendente(this._dittaId, "DTTGRD79P26H938X", "Dotti", "Galardo", Sesso.M, new DateTime(1979, 09, 26)),
                new Dipendente(this._dittaId, "RZUGLN62L66G774X", "Urzo", "Gigliana", Sesso.F, new DateTime(1962, 07, 26)),
                new Dipendente(this._dittaId, "PLSRLT00H28L276N", "Pulsoni", "Risoluto", Sesso.M, new DateTime(2000, 06, 28)),
                new Dipendente(this._dittaId, "BRNFTT00S60A278W", "Bernardi", "Fifetta", Sesso.F, new DateTime(2000, 11, 20)),
                new Dipendente(this._dittaId, "BRNLND67M25L385J", "Brunetti", "Alindo", Sesso.M, new DateTime(1967, 08, 25)),
                new Dipendente(this._dittaId, "BRGFLL82M53C908P", "Baragliu", "Finella", Sesso.F, new DateTime(1982, 08, 13)),
                new Dipendente(this._dittaId, "DTRSMN69B21G499Y", "Di tuoro ", "Osmano", Sesso.M, new DateTime(1969, 02, 21)),
                new Dipendente(this._dittaId, "LCNPLE61B15F219H", "Lucani", "Euplio", Sesso.M, new DateTime(1961, 02, 15))
            };
        }

        #endregion
    }
}
