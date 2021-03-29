using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Xunit;

namespace Ictx.WebApp.IntegrationTest
{
    public class IntegrationTest : IClassFixture<AppInstance>
    {
        protected readonly HttpClient HttpClient;

        public IntegrationTest(AppInstance appInstance)
        {
            this.HttpClient = appInstance.CreateClient();
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
