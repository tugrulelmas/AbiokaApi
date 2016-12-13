using System;
using System.Collections.Generic;
using System.Net;

namespace AbiokaApi.Infrastructure.Common.Exceptions.Adapters
{
    public class ArgumentNullExceptionAdapter : IExceptionAdapter
    {
        private ArgumentNullException exception;

        public ArgumentNullExceptionAdapter(ArgumentNullException exception) {
            this.exception = exception;
        }

        public IDictionary<string, string> ExtraHeaders => null;

        public HttpStatusCode HttpStatusCode => HttpStatusCode.InternalServerError;

        public object Content => exception.Message;
    }
}