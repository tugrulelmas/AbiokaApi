using System.Net;

namespace AbiokaApi.Infrastructure.Common.Exceptions
{
    public class DenialException : ValidationException
    {
        public DenialException(string errorCode)
            : this(HttpStatusCode.BadRequest, errorCode) {
        }

        public DenialException(HttpStatusCode statusCode, string errorCode)
            : base(errorCode ) {
            StatusCode = statusCode;
        }
    }
}