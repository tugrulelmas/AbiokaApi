using AbiokaApi.Infrastructure.Common.Dynamic;
using AbiokaApi.Infrastructure.Common.IoC;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace AbiokaApi.Host
{
    public class CustomDelegatingHandler : DelegatingHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken) {
            var dynamicHandlers = DependencyContainer.Container.ResolveAll<IDynamicHandler>();

            foreach (var dynamicHandlerItem in dynamicHandlers)
            {
                // TODO: pass the context parameter
                dynamicHandlerItem.BeforeSend(null);
            }
            try
            {
                return base.SendAsync(request, cancellationToken).ContinueWith(
                    (task) =>
                    {
                        foreach (var dynamicHandlerItem in dynamicHandlers)
                        {
                            // TODO: pass the context parameter
                            dynamicHandlerItem.AfterSend(null);
                        }

                        return task.Result;
                    }
                );
            }
            catch 
            {
                foreach (var dynamicHandlerItem in dynamicHandlers)
                {
                    // TODO: pass the context parameter
                    dynamicHandlerItem.OnException(null);
                }
                throw;
            }
        }
    }
}