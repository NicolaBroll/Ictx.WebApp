using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Ictx.WebApp.WebClient.Services;

namespace Ictx.WebApp.WebClient
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            var uri = new Uri("http://localhost:5002");

            builder.Services.AddHttpClient<DipendenteService>(client =>
            {
                client.BaseAddress = uri;
            });

            builder.Services.AddHttpClient<FoglioPresenzaService>(client =>
            {
                client.BaseAddress = uri;
            });

            builder.Services.AddHttpClient<FoglioPresenzaDettaglioGiornoService>(client =>
            {
                client.BaseAddress = uri;
            });            

            await builder.Build().RunAsync();
        }
    }
}
