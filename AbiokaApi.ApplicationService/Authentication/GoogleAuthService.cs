using AbiokaApi.ApplicationService.Abstractions;
using AbiokaApi.ApplicationService.Messaging;
using AbiokaApi.Domain;
using AbiokaApi.Domain.Repositories;
using AbiokaApi.Infrastructure.Common.ApplicationSettings;
using AbiokaApi.Infrastructure.Common.Authentication;
using AbiokaApi.Infrastructure.Common.Exceptions;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace AbiokaApi.ApplicationService.Authentication
{
    public class GoogleAuthService : IAuthService
    {
        private readonly IUserSecurityRepository userSecurityRepository;
        private readonly IRoleRepository roleRepository;
        private readonly IAbiokaToken abiokaToken;
        private readonly IHttpClient httpClient;

        private readonly string clientId;
        private readonly string clientSecret;

        public GoogleAuthService(IUserSecurityRepository userSecurityRepository, IRoleRepository roleRepository, IAbiokaToken abiokaToken, IConfigurationManager configurationManager, IHttpClient httpClient) {
            this.userSecurityRepository = userSecurityRepository;
            this.roleRepository = roleRepository;
            this.abiokaToken = abiokaToken;
            this.httpClient = httpClient;

            clientId = configurationManager.ReadAppSetting("GoogleClientId");
            clientSecret = configurationManager.ReadAppSetting("GoogleClientSecret");
        }

        public AuthProvider Provider => AuthProvider.Google;

        public async Task<string> LoginAsync(AuthRequest request) {
            var pairs = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("client_id", request.clientId),
                new KeyValuePair<string, string>("redirect_uri", request.redirectUri),
                new KeyValuePair<string, string>("client_secret", clientSecret),
                new KeyValuePair<string, string>("code", request.code),
                new KeyValuePair<string, string>("grant_type", "authorization_code"),
                new KeyValuePair<string, string>("access_type", "offline")
            };

            HttpRequestMessage tokenRequest = new HttpRequestMessage(HttpMethod.Post, new Uri("https://www.googleapis.com/oauth2/v4/token"));
            tokenRequest.Content = new FormUrlEncodedContent(pairs);
            tokenRequest.Content.Headers.TryAddWithoutValidation("ContentType ", "application/x-www-form-urlencoded");
            
            var tokenResult = await httpClient.SendAsync(tokenRequest);
            if (!tokenResult.IsSuccessStatusCode)
                throw AuthenticationException.InvalidCredential;

            var tokenResponse = await tokenResult.Content.ReadAsAsync<GoogleAccesTokenResponse>();

            HttpRequestMessage openIdRequest = new HttpRequestMessage(HttpMethod.Get, new Uri("https://www.googleapis.com/plus/v1/people/me/openIdConnect"));
            openIdRequest.Headers.Authorization = new AuthenticationHeaderValue(tokenResponse.token_type, tokenResponse.access_token);

            var openIdResult = await httpClient.SendAsync(openIdRequest);
            if (!openIdResult.IsSuccessStatusCode)
                throw AuthenticationException.InvalidCredential;

            var googleUser = await openIdResult.Content.ReadAsAsync<GoogleMeResponse>();

            var dbUser = userSecurityRepository.GetByEmail(googleUser.email);
            if (dbUser == null) {
                var userRole = roleRepository.GetByName("User");

                var userSecurity = new UserSecurity(
                    Guid.Empty,
                    googleUser.email,
                     AuthProvider.Google,
                    tokenResponse.access_token,
                    tokenResponse.refresh_token,
                    Guid.NewGuid().ToString(),
                    null,
                    null,
                    googleUser.locale == "tr" ? "tr" : "en", // TODO: check language correctly
                    googleUser.given_name,
                    googleUser.family_name,
                    googleUser.picture,
                    googleUser.gender == "male" ? Gender.Male : Gender.Female,
                    false,
                    true,
                    new List<Role> { userRole }
                );

                userSecurity.CreateToken(abiokaToken, tokenResponse.access_token);

                userSecurityRepository.Add(userSecurity);

                return userSecurity.Token;
            }

            if (dbUser.AuthProvider != AuthProvider.Google) {
                throw new DenialException($"UserIsRegisteredFor{dbUser.AuthProvider}", dbUser.Email);
            }

            dbUser.UpdateProviderRefreshToken(tokenResponse.refresh_token ?? tokenResponse.access_token);
            dbUser.CreateToken(abiokaToken, tokenResponse.access_token);
            userSecurityRepository.Update(dbUser);
            return dbUser.Token;
        }

        public async Task<string> RefreshTokenAsync(string refreshToken) {
            var dbUser = userSecurityRepository.GetByRefreshToken(refreshToken);
            if (dbUser == null) {
                throw AuthenticationException.InvalidCredential;
            }


            var pairs = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("client_id", clientId),
                new KeyValuePair<string, string>("client_secret", clientSecret),
                new KeyValuePair<string, string>("refresh_token", dbUser.ProviderRefreshToken),
                new KeyValuePair<string, string>("grant_type", "refresh_token")
            };

            HttpRequestMessage tokenRequest = new HttpRequestMessage(HttpMethod.Post, new Uri("https://www.googleapis.com/oauth2/v4/token"));
            tokenRequest.Content = new FormUrlEncodedContent(pairs);
            tokenRequest.Content.Headers.TryAddWithoutValidation("ContentType ", "application/x-www-form-urlencoded");
            
            var tokenResult = await httpClient.SendAsync(tokenRequest);
            if (!tokenResult.IsSuccessStatusCode)
                throw AuthenticationException.InvalidCredential;

            var tokenResponse = await tokenResult.Content.ReadAsAsync<GoogleAccesTokenResponse>();

            dbUser.CreateToken(abiokaToken, tokenResponse.access_token);
            userSecurityRepository.Update(dbUser);
            return dbUser.Token;
        }

        class GoogleMeResponse
        {
            public string kind { get; set; }

            public string gender { get; set; }

            public string sub { get; set; }

            public string name { get; set; }

            public string given_name { get; set; }

            public string family_name { get; set; }

            public string profile { get; set; }

            public string picture { get; set; }

            public string email { get; set; }

            public string locale { get; set; }
        }

        class GoogleAccesTokenResponse
        {
            public string access_token { get; set; }

            public string token_type { get; set; }

            public int expires_in { get; set; }

            public string refresh_token { get; set; }

            public string id_token { get; set; }
        }
    }
}
