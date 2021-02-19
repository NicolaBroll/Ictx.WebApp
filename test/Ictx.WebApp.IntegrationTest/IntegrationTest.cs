using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Ictx.WebApp.Api;
using Ictx.WebApp.Infrastructure.Data;
using System;

namespace Ictx.WebApp.IntegrationTest
{
    public class IntegrationTest
    {
        protected readonly HttpClient HttpClient;

        public IntegrationTest()
        {
            var appFactory = new WebApplicationFactory<Startup>()
                .WithWebHostBuilder(builder =>
                {
                    builder.ConfigureServices(services =>
                    {
                        services.RemoveAll(typeof(AppDbContext));
                        services.RemoveAll(typeof(DbContextOptions<AppDbContext>));
                        services.AddDbContext<AppDbContext>(options => { options.UseInMemoryDatabase("TestDb"); });
                    });
                });

            this.HttpClient = appFactory.CreateClient();
        }

        protected string GetVersionedUrl(string url, int version)
        {
            return url.Replace("{version:apiVersion}", version.ToString());
        }

        protected async Task AuthenticateAsync() 
        {
            this.HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", await GetJwtAsync());
        }

        private async Task<string> GetJwtAsync()
        {            
            return await Task.FromException<string>(new NotImplementedException());
        }
    }
}
