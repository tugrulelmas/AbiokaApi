using AbiokaApi.ApplicationService.Abstractions;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace AbiokaApi.ApplicationService.Implementations
{
    public class CustomHttpClient : IHttpClient
    {
        private readonly HttpClient client;

        public CustomHttpClient() {
            client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public Task<HttpResponseMessage> GetAsync(string requestUri) => client.GetAsync(requestUri);

        public Task<HttpResponseMessage> SendAsync(HttpRequestMessage request) => client.SendAsync(request);
    }
}
