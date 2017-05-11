using AbiokaApi.Infrastructure.Common.Exceptions;
using System.Net;

namespace AbiokaApi.Infrastructure.Common.Authentication
{
    public class AuthenticationException : DenialException
    {
        public AuthenticationException(string errorCode)
            : this(HttpStatusCode.Unauthorized, errorCode) {
        }

        public AuthenticationException(HttpStatusCode httpStatusCode, string errorCode)
            : base(httpStatusCode, errorCode) {
        }

        public static AuthenticationException TokenExpired => new AuthenticationException("Token is expired");

        public static AuthenticationException InvalidCredential => new AuthenticationException(HttpStatusCode.BadRequest, "InvalidCredentials");

        public static AuthenticationException MissingCredential => new AuthenticationException(HttpStatusCode.BadRequest, "MissingCredentials");
    }
}
