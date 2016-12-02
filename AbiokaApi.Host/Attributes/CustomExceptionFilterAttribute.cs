using AbiokaApi.Infrastructure.Common.Dynamic;
using AbiokaApi.Infrastructure.Common.IoC;
using System.Linq;
using System.Web.Http.Filters;

namespace AbiokaApi.Host.Attributes
{
    public class CustomExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext context) {
            var dynamicHandlers = DependencyContainer.Container.ResolveAll<IDynamicHandler>().OrderBy(d => d.Order);
            IExceptionContext exceptionContext = new ExceptionContext(context);
            foreach (var dynamicHandlerItem in dynamicHandlers) {
                dynamicHandlerItem.OnException(exceptionContext);
            }
        }
    }
}