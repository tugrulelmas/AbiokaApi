using System.Collections.Generic;
using System.Net;

namespace AbiokaApi.Infrastructure.Common.Exceptions.Adapters
{
    public class ApiExceptionAdapter : IExceptionAdapter
    {
        private ApiException apiException;

        public ApiExceptionAdapter(ApiException apiException) {
            this.apiException = apiException;
        }

        public IDictionary<string, string> ExtraHeaders => apiException.ExtraHeaders;

        public HttpStatusCode HttpStatusCode => apiException.StatusCode;

        public object Content => apiException.ContentValue;
    }
}