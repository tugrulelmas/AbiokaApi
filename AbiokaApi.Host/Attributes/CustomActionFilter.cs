using AbiokaApi.Infrastructure.Common.Dynamic;
using AbiokaApi.Infrastructure.Common.IoC;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace AbiokaApi.Host.Attributes
{
    public class CustomActionFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext) {
            IRequestContext requestContext = new RequestContext(actionContext.Request);

            foreach (var dynamicHandlerItem in dynamicHandlers) {
                dynamicHandlerItem.BeforeSend(requestContext);
            }
            
            base.OnActionExecuting(actionContext);
        }

        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext) {
            base.OnActionExecuted(actionExecutedContext);

            if (actionExecutedContext.Exception == null) {
                foreach (var dynamicHandlerItem in dynamicHandlers) {
                    dynamicHandlerItem.AfterSend(null);
                }
            }
            else {
                IExceptionContext exceptionContext = new ExceptionContext(actionExecutedContext);
                foreach (var dynamicHandlerItem in dynamicHandlers) {
                    dynamicHandlerItem.OnException(exceptionContext);
                }
            }
        }

        private IEnumerable<IDynamicHandler> dynamicHandlers => DependencyContainer.Container.ResolveAll<IDynamicHandler>().OrderBy(d => d.Order);
    }
}