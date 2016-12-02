using AbiokaApi.Infrastructure.Common.Exceptions;
using System.Net;

namespace AbiokaApi.Infrastructure.Framework.Authentication
{
    public class AuthenticationException : DenialException
    {
        public AuthenticationException(string errorCode)
            : base(HttpStatusCode.Unauthorized, errorCode) {
        }

        public static AuthenticationException TokenExpired => new AuthenticationException("Token is expired");

        public static AuthenticationException InvalidCredential => new AuthenticationException("Invalid credentials");

        public static AuthenticationException MissingCredential => new AuthenticationException("Missing credentials");
    }
}
