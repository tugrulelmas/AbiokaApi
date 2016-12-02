using System;
using System.Collections.Generic;
using System.Net;

namespace AbiokaApi.Infrastructure.Common.Exceptions
{
    public abstract class ApiException : Exception
    {
        public ApiException(string message)
            : this(message, null) {
        }

        public ApiException(string message, Exception innerException)
            : base(message, innerException) {
            StatusCode = HttpStatusCode.InternalServerError; 
            ExtraHeaders = new Dictionary<string, string>();
        }

        public object ContentValue { get; protected set; }

        public HttpStatusCode StatusCode { get; protected set; }

        public IDictionary<string, string> ExtraHeaders { get; protected set; }
    }
}