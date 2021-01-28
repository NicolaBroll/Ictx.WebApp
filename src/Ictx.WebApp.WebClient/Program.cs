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

            builder.Services.AddHttpClient<DipendenteService>(client =>
            {
                client.BaseAddress = new Uri("http://localhost:5002");
            });

            builder.Services.AddHttpClient<FoglioPresenzaService>(client =>
            {
                client.BaseAddress = new Uri("http://localhost:5002");
            });

            await builder.Build().RunAsync();
        }
    }
}
