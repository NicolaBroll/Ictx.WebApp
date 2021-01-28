using Ictx.WebApp.Shared.Dtos;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Ictx.WebApp.WebClient.Services
{
    public class DipendenteService
    {
        private readonly HttpClient _httpClient;
        private readonly string     _controllerPath;

        public DipendenteService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _controllerPath = "api/dipendente";
        }

        public async Task<IEnumerable<DipendenteDto>> Get()
        {
            return await _httpClient.GetJsonAsync<DipendenteDto[]>(this._controllerPath);
        }

        public async Task<DipendenteDto> GetById(int id)
        {
            return await _httpClient.GetJsonAsync<DipendenteDto>($"{this._controllerPath}/{id}");
        }
    }
}
