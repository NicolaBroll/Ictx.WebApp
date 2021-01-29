using System.Net.Http;
using System.Threading.Tasks;

namespace Ictx.WebApp.WebClient.Services
{
    public class FoglioPresenzaDettaglioGiornoService
    {
        private readonly HttpClient _httpClient;
        private readonly string     _controllerPath;

        public FoglioPresenzaDettaglioGiornoService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _controllerPath = "api/foglioPresenzaDettaglioGiorno";
        }

        public async Task Delete(int id)
        {
            var url = $"{_controllerPath}/{id}";
            await _httpClient.DeleteAsync(url);
        }        
    }
}
