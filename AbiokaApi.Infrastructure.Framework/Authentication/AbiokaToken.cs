using AbiokaApi.Infrastructure.Common.Authentication;
using System;

namespace AbiokaApi.Infrastructure.Framework.Authentication
{
    public class AbiokaToken : IAbiokaToken
    {
        private const string key = "_A4b%i+oKa$_";

        public string Encode(UserClaim userClaim) {
            var defaultExpMinutes = new TimeSpan(0, 20, 0).TotalMinutes; 
            return Encode(userClaim, defaultExpMinutes);
        }

        public string Encode(UserClaim userClaim, double expirationMinutes) {
            var utc0 = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            var issueTime = DateTime.UtcNow;

            var iat = (int)issueTime.Subtract(utc0).TotalSeconds;
            var exp = (int)issueTime.AddMinutes(expirationMinutes).Subtract(utc0).TotalSeconds;

            var payload = new TokenPayload {
                iss = "abioka",
                exp = exp,
                iat = iat,
                email = userClaim.Email,
                id = userClaim.Id,
                provider = userClaim.Provider.ToString(),
                roles = userClaim.Roles,
                refresh_token = userClaim.RefreshToken,
                language = userClaim.Language
            };

            return JsonWebToken.Encode(payload, key, JwtHashAlgorithm.HS256);
        }

        public TokenPayload Decode(string token) {
            var payload = JsonWebToken.DecodeToObject<TokenPayload>(token, key);
            return payload;
        }
    }
}
