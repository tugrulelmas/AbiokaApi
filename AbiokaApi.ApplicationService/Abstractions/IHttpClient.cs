using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AbiokaApi.ApplicationService.Abstractions
{
    public interface IHttpClient
    {
        Task<HttpResponseMessage> GetAsync(string requestUri);

        Task<HttpResponseMessage> SendAsync(HttpRequestMessage request);
    }
}
