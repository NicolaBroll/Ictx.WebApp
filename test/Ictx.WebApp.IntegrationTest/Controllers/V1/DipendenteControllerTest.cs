using Xunit;
using FluentAssertions;

using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using Ictx.WebApp.Api.Models;
using Ictx.WebApp.Api.Controllers.V1;
using Ictx.WebApp.Core.Models;

namespace Ictx.WebApp.IntegrationTest.Controllers.V1
{
    public class DipendenteControllerTest: IntegrationTest
    {
        private readonly int _version;
        private readonly int _dittaId;
        private readonly IReadOnlyList<DipendenteDto> _lstDipendenteDto;

        public DipendenteControllerTest(AppInstance instance) : base(instance)
        {
            this._version = 1;
            this._dittaId = 1;

            this._lstDipendenteDto = GetListaDipendentiFake();
        }

        #region GET

        /// <summary>
        /// Verifica che la risposta sia corretta in assenza di dipendenti.
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task GetAll_WithOutDipendenti_ReturnEmptyResponse()
        {
            // Arrange.
            var url = GetVersionedUrl(ApiRoutesV1.DipendenteRoute.Get, _version);

            // Act.
            var response = await HttpClient.GetAsync(url);

            // Assert.
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var parsedRespose = await response.Content.ReadAsAsync<PageResultDto<DipendenteDto>>();

            parsedRespose.Data.Count().Should().Be(0);
            parsedRespose.TotalCount.Should().Be(0);
        }

        /// <summary>
        /// Inserisce i dipendenti in DB e successivamente richiede X dipendenti verificando sia il total count 
        /// sia il count effettivo dei dipendenti ritornati dal server.
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task GetAll_WithDipendenti_ReturnXDipendenti()
        {
            // Arrange.  
            var dipendentiRequest = 16;

            var postTasks = new List<Task>();

            this._lstDipendenteDto.ToList().ForEach(x => postTasks.Add(PostDipendente(x)));

            Task.WaitAll(postTasks.ToArray());

            var url = GetVersionedUrl(ApiRoutesV1.DipendenteRoute.Get, _version);
            url += $"?dittaId={this._dittaId}&page=1&pageSize={dipendentiRequest}";

            // Act.
            var response = await HttpClient.GetAsync(url);

            // Assert.
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var parsedRespose = await response.Content.ReadAsAsync<PageResult<DipendenteDto>>();

            parsedRespose.TotalCount.Should().BeGreaterOrEqualTo(this._lstDipendenteDto.Count()); // Dipendenti totali in database.
            parsedRespose.Data.Count().Should().Be(dipendentiRequest); // Dipendenti totali richiesti.
        }

        #endregion

        #region GET ONE

        /// <summary>
        /// Verifica che venga resttuito 404 con il relativo messaggio d'errore nel caso il dipendente non sia presente.
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task GetOne_WithOutDipendente_ReturnEmptyResponse()
        {
            // Arrange.
            var url = GetVersionedUrl(ApiRoutesV1.DipendenteRoute.GetById, _version);
            var dipendenteId = 99999999;

            // Act.
            var response = await HttpClient.GetAsync(url.Replace("{id}", dipendenteId.ToString()));

            // Assert.
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);

            var parsedRespose = await response.Content.ReadAsAsync<ErrorResponseDto>();

            parsedRespose.Message.Should().NotBeNullOrEmpty();
        }

        /// <summary>
        /// Crea il dipendente e verifica che sia possibile leggerlo con risposta 200.
        /// Viene fatto un compare campo per campo verificando che l'oggetto restituito sia identico all'oggetto creato.
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task GetOne_WithDipendente_ReturnResponse()
        {
            // Arrange.
            var dipendenteToCreate = this._lstDipendenteDto.First(); 
            var dipendenteCreatedResponse = await PostDipendente(dipendenteToCreate);
            var dipendenteCreated = await dipendenteCreatedResponse.Content.ReadAsAsync<DipendenteDto>();

            var url = GetVersionedUrl(ApiRoutesV1.DipendenteRoute.GetById, _version);

            // Act.
            var response = await HttpClient.GetAsync(url.Replace("{id}", dipendenteCreated.Id.ToString()));

            // Assert.
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var parsedRespose = await response.Content.ReadAsAsync<DipendenteDto>();

            parsedRespose.Id.Should().Be(dipendenteCreated.Id);
            parsedRespose.DittaId.Should().Be(dipendenteCreated.DittaId);
            parsedRespose.CodiceFiscale.Should().Be(dipendenteCreated.CodiceFiscale);
            parsedRespose.Cognome.Should().Be(dipendenteCreated.Cognome);
            parsedRespose.Nome.Should().Be(dipendenteCreated.Nome);
            parsedRespose.Sesso.Should().Be(dipendenteCreated.Sesso);
            parsedRespose.DataNascita.Should().Be(dipendenteCreated.DataNascita);
        }

        #endregion

        #region POST

        /// <summary>
        /// Crea il dipendente e verifica che la risposta sia 200.
        /// Viene fatto un compare campo per campo verificando che l'oggetto restituito sia identico all'oggetto creato.
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task CreateOne_ReturnOk()
        {
            // Arrange.
            var dipendenteToCreate = this._lstDipendenteDto.First();

            // Act.
            var response = await PostDipendente(dipendenteToCreate);

            // Assert.
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var parsedRespose = await response.Content.ReadAsAsync<DipendenteDto>();

            parsedRespose.DittaId.Should().Be(dipendenteToCreate.DittaId);
            parsedRespose.CodiceFiscale.ToUpper().Should().Be(dipendenteToCreate.CodiceFiscale.ToUpper());
            parsedRespose.Cognome.ToUpper().Should().Be(dipendenteToCreate.Cognome.ToUpper());
            parsedRespose.Nome.ToUpper().Should().Be(dipendenteToCreate.Nome.ToUpper());
            parsedRespose.Sesso.Should().Be(dipendenteToCreate.Sesso);
            parsedRespose.DataNascita.Should().Be(dipendenteToCreate.DataNascita);
        }

        /// <summary>
        /// Crea il dipendente e verifica che la validazione per il codice fiscale funzioni.
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task CreateOne_WithCodiceFiscaleEmpty_ReturnBadRequest()
        {
            // Arrange.
            var dipendenteToCreate = this._lstDipendenteDto.First();

            dipendenteToCreate.CodiceFiscale = string.Empty;

            // Act.
            var response = await PostDipendente(dipendenteToCreate);

            // Assert.
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        /// <summary>
        /// Crea il dipendente e verifica che la validazione per il nome funzioni.
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task CreateOne_WithNomeEmpty_ReturnBadRequest()
        {
            // Arrange.
            var dipendenteToCreate = this._lstDipendenteDto.First();

            dipendenteToCreate.Nome = string.Empty;

            // Act.
            var response = await PostDipendente(dipendenteToCreate);

            // Assert.
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        /// <summary>
        /// Crea il dipendente e verifica che la validazione per il cognome funzioni.
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task CreateOne_WithCognomeEmpty_ReturnBadRequest()
        {
            // Arrange.
            var dipendenteToCreate = this._lstDipendenteDto.First();

            dipendenteToCreate.Cognome = string.Empty;

            // Act.
            var response = await PostDipendente(dipendenteToCreate);

            // Assert.
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        /// <summary>
        /// Crea il dipendente e verifica che la validazione per la ditta funzioni.
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task CreateOne_WithOutDitta_ReturnBadRequest()
        {
            // Arrange.
            var dipendenteToCreate = this._lstDipendenteDto.First();

            dipendenteToCreate.DittaId = 0;

            // Act.
            var response = await PostDipendente(dipendenteToCreate);

            // Assert.
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        #endregion

        #region PUT

        /// <summary>
        /// Modifica il dipendente e verifica che la risposta sia 200.
        /// Viene fatto un compare campo per campo verificando che l'oggetto restituito sia identico all'oggetto modificato lato client.
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task EditOne_WithDipendente_ReturnResponse()
        {
            // Arrange.
            var dipendenteToCreate = this._lstDipendenteDto.First();
            var dipendenteCreatedResponse = await PostDipendente(dipendenteToCreate);
            var dipendenteCreated = await dipendenteCreatedResponse.Content.ReadAsAsync<DipendenteDto>();

            dipendenteCreated.CodiceFiscale = "MMMMMMMMMMMMMMMM";
            dipendenteCreated.Cognome = "MMMMMMMMMMMMMMMM";
            dipendenteCreated.Nome = "MMMMMMMMMMMMMMMM";
            dipendenteCreated.Sesso = "M";
            dipendenteCreated.DataNascita = DateTime.MinValue;

            var url = GetVersionedUrl(ApiRoutesV1.DipendenteRoute.Put, _version);

            // Act.
            var response = await HttpClient.PutAsJsonAsync(url.Replace("{id}", dipendenteCreated.Id.ToString()), dipendenteCreated);

            // Assert.
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var parsedRespose = await response.Content.ReadAsAsync<DipendenteDto>();

            parsedRespose.Id.Should().Be(dipendenteCreated.Id);
            parsedRespose.DittaId.Should().Be(dipendenteCreated.DittaId);
            parsedRespose.CodiceFiscale.ToUpper().Should().Be(dipendenteCreated.CodiceFiscale.ToUpper());
            parsedRespose.Cognome.ToUpper().Should().Be(dipendenteCreated.Cognome.ToUpper());
            parsedRespose.Nome.ToUpper().Should().Be(dipendenteCreated.Nome.ToUpper());
            parsedRespose.Sesso.Should().Be(dipendenteCreated.Sesso);
            parsedRespose.DataNascita.Should().Be(dipendenteCreated.DataNascita);
        }

        /// <summary>
        /// Verifica che venga restituito 404 nel caso il dipendente non sia presente in DB.
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task EditOne_WithoutDipendente_ReturnNotFound()
        {
            // Arrange.
            var dipendenteToCreate = this._lstDipendenteDto.First();
            var id = 999;

            var url = GetVersionedUrl(ApiRoutesV1.DipendenteRoute.Put, _version);

            // Act.
            var response = await HttpClient.PutAsJsonAsync(url.Replace("{id}", id.ToString()), dipendenteToCreate);

            // Assert.
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        /// <summary>
        /// Verifica che venga restituito bad request se la ditta non viene trovata in DB.
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task EditOne_WithDipendenteWithOutDitta_ReturnBadRequest()
        {
            // Arrange.
            var dipendenteToCreate = this._lstDipendenteDto.First();
            var dipendenteCreatedResponse = await PostDipendente(dipendenteToCreate);
            var dipendenteCreated = await dipendenteCreatedResponse.Content.ReadAsAsync<DipendenteDto>();

            dipendenteCreated.DittaId = 99999;

            var url = GetVersionedUrl(ApiRoutesV1.DipendenteRoute.Put, _version);

            // Act.
            var response = await HttpClient.PutAsJsonAsync(url.Replace("{id}", dipendenteCreated.Id.ToString()), dipendenteCreated);

            // Assert.
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        #endregion

        #region DELETE

        /// <summary>
        /// Crea un dipendente.
        /// Verifica che la delete restituisca 200 per il dipendente creato in precedenza.
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task DeleteOne_WithDipendente_ReturnOk()
        {
            // Arrange.
            var dipendenteToCreate = this._lstDipendenteDto.First();
            var dipendenteCreatedResponse = await PostDipendente(dipendenteToCreate);
            var dipendenteCreated = await dipendenteCreatedResponse.Content.ReadAsAsync<DipendenteDto>();

            var url = GetVersionedUrl(ApiRoutesV1.DipendenteRoute.Delete, _version);

            // Act.
            var response = await HttpClient.DeleteAsync(url.Replace("{id}", dipendenteCreated.Id.ToString()));

            // Assert.
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        /// <summary>
        /// Verifica che venga resttuito 404 con il relativo messaggio d'errore nel caso il dipendente non sia presente.
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task DeleteOne_WithoutDipendente_ReturnNotFound()
        {
            // Arrange.
            var id = 999999;
            var url = GetVersionedUrl(ApiRoutesV1.DipendenteRoute.Delete, _version);

            // Act.
            var response = await HttpClient.DeleteAsync(url.Replace("{id}", id.ToString()));

            // Assert.
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);

            var parsedRespose = await response.Content.ReadAsAsync<ErrorResponseDto>();

            parsedRespose.Message.Should().NotBeNullOrEmpty();
        }

        #endregion

        #region UTILS

        private List<DipendenteDto> GetListaDipendentiFake()
        {
            return new List<DipendenteDto>()
            {
                new DipendenteDto(this._dittaId, "PRDFRN52D67D316U", "Apreda", "Fabrina", "F", new DateTime(1952, 04, 27)),
                new DipendenteDto(this._dittaId, "MRTMND85P55F365E", "Martino", "Merinda", "F", new DateTime(1985, 09, 15)),
                new DipendenteDto(this._dittaId, "MRSSNZ92P15M159G", "Morselli", "Assenzio", "M", new DateTime(1992, 09, 15)),
                new DipendenteDto(this._dittaId, "BNGSRA91L12F329R", "Buongrazio", "Saro", "M", new DateTime(1991, 07, 12)),
                new DipendenteDto(this._dittaId, "DMLTLC87B26C330A", "D'emilia", "Italico", "M", new DateTime(1987, 02, 26)),
                new DipendenteDto(this._dittaId, "NFNVNT76A10A160I", "Infante", "Valento", "M", new DateTime(1976, 01, 10)),
                new DipendenteDto(this._dittaId, "DLGTTL67E54A761P", "Delgado", "Attala", "F", new DateTime(1967, 05, 14)),
                new DipendenteDto(this._dittaId, "WLLDRO68H17B361E", "Williams", "Dorio", "M", new DateTime(1968, 06, 17)),
                new DipendenteDto(this._dittaId, "FRRLRT94L53H437X", "Ferrari", "Laurita", "F", new DateTime(1994, 07, 13)),
                new DipendenteDto(this._dittaId, "BNASSM65B53B692O", "Baiano", "Sigismonda", "F", new DateTime(1965, 02, 13)),
                new DipendenteDto(this._dittaId, "CZZRMD99B01L601R", "Cozzolino", "Remido", "M", new DateTime(1999, 02, 01)),
                new DipendenteDto(this._dittaId, "BRNPMT55L41E388X", "Brunetti", "Primetta", "F", new DateTime(1955, 07, 01)),
                new DipendenteDto(this._dittaId, "DTTGRD79P26H938X", "Dotti", "Galardo", "M", new DateTime(1979, 09, 26)),
                new DipendenteDto(this._dittaId, "RZUGLN62L66G774X", "Urzo", "Gigliana", "F", new DateTime(1962, 07, 26)),
                new DipendenteDto(this._dittaId, "PLSRLT00H28L276N", "Pulsoni", "Risoluto", "M", new DateTime(2000, 06, 28)),
                new DipendenteDto(this._dittaId, "BRNFTT00S60A278W", "Bernardi", "Fifetta", "F", new DateTime(2000, 11, 20)),
                new DipendenteDto(this._dittaId, "BRNLND67M25L385J", "Brunetti", "Alindo", "M", new DateTime(1967, 08, 25)),
                new DipendenteDto(this._dittaId, "BRGFLL82M53C908P", "Baragliu", "Finella", "F", new DateTime(1982, 08, 13)),
                new DipendenteDto(this._dittaId, "DTRSMN69B21G499Y", "Di tuoro ", "Osmano", "M", new DateTime(1969, 02, 21)),
                new DipendenteDto(this._dittaId, "LCNPLE61B15F219H", "Lucani", "Euplio", "M", new DateTime(1961, 02, 15))
            };
        }

        private async Task<HttpResponseMessage> PostDipendente(DipendenteDto dipendenteToCreate)
        {
            var url = GetVersionedUrl(ApiRoutesV1.DipendenteRoute.Post, _version);

            var rsponse = await HttpClient.PostAsJsonAsync(url, dipendenteToCreate);

            return rsponse;
        }

        #endregion
    }
}
