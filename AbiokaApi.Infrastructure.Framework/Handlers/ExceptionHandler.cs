using AbiokaApi.Infrastructure.Common.Dynamic;
using AbiokaApi.Infrastructure.Common.Exceptions.Adapters;
using System.Net.Http;
using System.Web.Http.Filters;

namespace AbiokaApi.Infrastructure.Framework.Handlers
{
    public class ExceptionHandler : IDynamicHandler
    {
        private readonly IExceptionAdapterFactory exceptionAdapterFactory;

        public ExceptionHandler(IExceptionAdapterFactory exceptionAdapterFactory) {
            this.exceptionAdapterFactory = exceptionAdapterFactory;
        }

        public short Order => 100;

        public void AfterSend(IResponseContext responseContext) {
        }

        public void BeforeSend(IRequestContext requestContext) {
        }

        public void OnException(IExceptionContext exceptionContext) {
            var context =  (HttpActionExecutedContext)exceptionContext.Context;
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
