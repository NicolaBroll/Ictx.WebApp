using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Ictx.WebApp.WebClient.Services;
using Fluxor;
using System.Reflection;

namespace Ictx.WebApp.WebClient
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            // Add Fluxor
            builder.Services.AddFluxor(options => 
            {
                options.ScanAssemblies(Assembly.GetExecutingAssembly());
                options.UseReduxDevTools();
            });

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
            
            // Add custom application services
            builder.Services.AddScoped<StateFacade>();

            await builder.Build().RunAsync();
        }
    }
}
