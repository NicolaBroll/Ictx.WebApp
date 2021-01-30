using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Ictx.WebApp.Shared.Dtos;

namespace Ictx.WebApp.WebClient.Services
{
    public class FoglioPresenzaService
    {
        private readonly HttpClient _httpClient;
        private readonly string     _controllerPath;

        public FoglioPresenzaService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _controllerPath = "api/foglioPresenza";
        }

        public async Task<FoglioPresenzaDto> Get(int dipendenteId, int anno, int mese)
        {
            var url = $"{_controllerPath}?dipendenteId={dipendenteId}&anno={anno}&mese={mese}";
            return await _httpClient.GetJsonAsync<FoglioPresenzaDto>(url);
        }        
    }
}
