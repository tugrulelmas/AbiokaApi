using AbiokaApi.Infrastructure.Common.Dynamic;
using AbiokaApi.Infrastructure.Common.Exceptions;
using AbiokaApi.Infrastructure.Common.Helper;
using System;
using System.Web.Http.Filters;

namespace AbiokaApi.Infrastructure.Framework.RestHelper
{
    public class ExceptionLogResolver : IExceptionLogResolver
    {
        private readonly ICurrentContext currentContext;

        public ExceptionLogResolver(ICurrentContext currentContext) {
            this.currentContext = currentContext;
        }

        public ExceptionLog Resolve(IExceptionContext exceptionContext) {
            var context = (HttpActionExecutedContext)exceptionContext.Context;
            var errorCode = string.Empty;
            if(context.Exception is DenialException) {
                errorCode = ((ExceptionContent)((DenialException)context.Exception).ContentValue).ErrorCode;
            }

            return new ExceptionLog(context.Exception.Source, context.Request.ToString(), context.Exception.GetType().Name, errorCode, context.Exception.ToString(), 
                currentContext.Current.Principal?.Id ?? Guid.Empty,
                currentContext.Current.IP);
        }
    }
}
