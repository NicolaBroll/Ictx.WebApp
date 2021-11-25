using Xunit;
using FluentAssertions;

using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using Ictx.WebApp.Api.Models;
using Ictx.WebApp.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace Ictx.WebApp.IntegrationTest.Controllers.V1
{
    public class DipendenteControllerTest: IntegrationTest
    {
        private readonly int _version;
        private readonly string _controllerName;
        private readonly List<DipendenteDto> _lstDipendenteDto;

        public DipendenteControllerTest(AppInstance instance) : base(instance)
        {
            this._version = 1;
            this._controllerName = "Dipendente";
            this._lstDipendenteDto = GetListaDipendentiFake();
        }

        #region GET

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

            var url = GetUrl();
            url += $"?page=1&pageSize={dipendentiRequest}";

            // Act.
            var response = await HttpClient.GetAsync(url);

            // Assert.
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var parsedRespose = await response.Content.ReadAsAsync<PageResult<DipendenteDto>>();

            parsedRespose.TotalCount.Should().BeGreaterOrEqualTo(this._lstDipendenteDto.Count); // Dipendenti totali in database.
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
            var dipendenteId = 99999999;
            var url = GetUrl() + "/" + dipendenteId;

            // Act.
            var response = await HttpClient.GetAsync(url);

            // Assert.
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);

            var parsedRespose = await response.Content.ReadAsAsync<ProblemDetails>();

            parsedRespose.Detail.Should().NotBeNullOrEmpty();
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

            var url = GetUrl() + "/" + dipendenteCreated.Id;

            // Act.
            var response = await HttpClient.GetAsync(url);

            // Assert.
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var parsedRespose = await response.Content.ReadAsAsync<DipendenteDto>();

            parsedRespose.Id.Should().Be(dipendenteCreated.Id);
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
            response.StatusCode.Should().Be(HttpStatusCode.Created);

            var parsedRespose = await response.Content.ReadAsAsync<DipendenteDto>();

            parsedRespose.Cognome.Should().Be(dipendenteToCreate.Cognome);
            parsedRespose.Nome.Should().Be(dipendenteToCreate.Nome);
            parsedRespose.Sesso.Should().Be(dipendenteToCreate.Sesso);
            parsedRespose.DataNascita.Should().Be(dipendenteToCreate.DataNascita);
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

            dipendenteCreated.Cognome = "MMMMMMMMMMMMMMMM";
            dipendenteCreated.Nome = "MMMMMMMMMMMMMMMM";
            dipendenteCreated.Sesso = "M";
            dipendenteCreated.DataNascita = DateTime.MinValue;

            var url = GetUrl() + "/" + dipendenteCreated.Id.ToString();

            // Act.
            var response = await HttpClient.PutAsJsonAsync(url, dipendenteCreated);

            // Assert.
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var parsedRespose = await response.Content.ReadAsAsync<DipendenteDto>();

            parsedRespose.Id.Should().Be(dipendenteCreated.Id);
            parsedRespose.Cognome.Should().Be(dipendenteCreated.Cognome);
            parsedRespose.Nome.Should().Be(dipendenteCreated.Nome);
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

            var url = GetUrl() + "/" + id;

            // Act.
            var response = await HttpClient.PutAsJsonAsync(url, dipendenteToCreate);

            // Assert.
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
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

            var url = GetUrl() + "/" + dipendenteCreated.Id.ToString();

            // Act.
            var response = await HttpClient.DeleteAsync(url);

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
            var url = GetUrl() + "/" + id;

            // Act.
            var response = await HttpClient.DeleteAsync(url.Replace("{id}", id.ToString()));

            // Assert.
            ; response.StatusCode.Should().Be(HttpStatusCode.NotFound);

            var parsedRespose = await response.Content.ReadAsAsync<ProblemDetails>();

            parsedRespose.Detail.Should().NotBeNullOrEmpty();
        }

        #endregion

        #region UTILS

        private List<DipendenteDto> GetListaDipendentiFake() => 
            new List<DipendenteDto>()
            {
                new DipendenteDto("Apreda", "Fabrina", "F", new DateTime(1952, 04, 27)),
                new DipendenteDto("Martino", "Merinda", "F", new DateTime(1985, 09, 15)),
                new DipendenteDto("Morselli", "Assenzio", "M", new DateTime(1992, 09, 15)),
                new DipendenteDto("Buongrazio", "Saro", "M", new DateTime(1991, 07, 12)),
                new DipendenteDto("D'emilia", "Italico", "M", new DateTime(1987, 02, 26)),
                new DipendenteDto("Infante", "Valento", "M", new DateTime(1976, 01, 10)),
                new DipendenteDto("Delgado", "Attala", "F", new DateTime(1967, 05, 14)),
                new DipendenteDto("Williams", "Dorio", "M", new DateTime(1968, 06, 17)),
                new DipendenteDto("Ferrari", "Laurita", "F", new DateTime(1994, 07, 13)),
                new DipendenteDto("Baiano", "Sigismonda", "F", new DateTime(1965, 02, 13)),
                new DipendenteDto("Cozzolino", "Remido", "M", new DateTime(1999, 02, 01)),
                new DipendenteDto("Brunetti", "Primetta", "F", new DateTime(1955, 07, 01)),
                new DipendenteDto("Dotti", "Galardo", "M", new DateTime(1979, 09, 26)),
                new DipendenteDto("Urzo", "Gigliana", "F", new DateTime(1962, 07, 26)),
                new DipendenteDto("Pulsoni", "Risoluto", "M", new DateTime(2000, 06, 28)),
                new DipendenteDto("Bernardi", "Fifetta", "F", new DateTime(2000, 11, 20)),
                new DipendenteDto("Brunetti", "Alindo", "M", new DateTime(1967, 08, 25)),
                new DipendenteDto("Baragliu", "Finella", "F", new DateTime(1982, 08, 13)),
                new DipendenteDto("Di tuoro ", "Osmano", "M", new DateTime(1969, 02, 21)),
                new DipendenteDto("Lucani", "Euplio", "M", new DateTime(1961, 02, 15))
            };


        private async Task<HttpResponseMessage> PostDipendente(DipendenteDto dipendenteToCreate)
        {
            string url = GetUrl();

            var rsponse = await HttpClient.PostAsJsonAsync(url, dipendenteToCreate);

            return rsponse;
        }

        private string GetUrl()
        {
            return $"/api/v{this._version}/{this._controllerName}";
        }

        #endregion
    }
}
