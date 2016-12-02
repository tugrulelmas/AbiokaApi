using System;
using System.Collections.Generic;
using System.Net;

namespace AbiokaApi.Infrastructure.Common.Exceptions.Adapters
{
    public class ExceptionAdapter : IExceptionAdapter
    {
        private Exception exception;

        public ExceptionAdapter(Exception exception) {
            this.exception = exception;
        }

        public IDictionary<string, string> ExtraHeaders => null;

        public HttpStatusCode HttpStatusCode => HttpStatusCode.InternalServerError;

        public object Content => exception.ToString();
    }
}