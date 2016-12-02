using AbiokaApi.Infrastructure.Common.Exceptions.Adapters;
using System.Net.Http;
using System.Web.Http.Filters;

namespace AbiokaApi.Host.Attributes
{
    public class CustomExceptionFilterAttribute : ExceptionFilterAttribute
    {
        private readonly IExceptionAdapterFactory exceptionAdapterFactory;

        public CustomExceptionFilterAttribute(IExceptionAdapterFactory exceptionAdapterFactory) {
            this.exceptionAdapterFactory = exceptionAdapterFactory;
        }

        public override void OnException(HttpActionExecutedContext context) {
            IExceptionAdapter exceptionAdapter = exceptionAdapterFactory.GetAdapter(context.Exception);
            var response = context.Request.CreateResponse(exceptionAdapter.HttpStatusCode, exceptionAdapter.Content);
            
            if (exceptionAdapter.ExtraHeaders != null) {
                foreach (var headerItem in exceptionAdapter.ExtraHeaders) {
                    response.Headers.Add(headerItem.Key, headerItem.Value);
                }
            }
            context.Response = response;
        }
    }
}