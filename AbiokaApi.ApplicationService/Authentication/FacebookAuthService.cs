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
using System.Text;
using System.Threading.Tasks;

namespace AbiokaApi.ApplicationService.Authentication
{
    public class FacebookAuthService : IAuthService
    {
        private readonly IUserSecurityRepository userSecurityRepository;
        private readonly IRoleRepository roleRepository;
        private readonly IAbiokaToken abiokaToken;
        private readonly IHttpClient httpClient;

        private readonly string clientId;
        private readonly string clientSecret;

        public FacebookAuthService(IUserSecurityRepository userSecurityRepository, IRoleRepository roleRepository, IAbiokaToken abiokaToken, IConnectionStringRepository connectionStringRepository, IHttpClient httpClient) {
            this.userSecurityRepository = userSecurityRepository;
            this.roleRepository = roleRepository;
            this.abiokaToken = abiokaToken;
            this.httpClient = httpClient;

            clientId = connectionStringRepository.ReadAppSetting("FacebookClientId");
            clientSecret = connectionStringRepository.ReadAppSetting("FacebookClientSecret");
        }

        public AuthProvider Provider => AuthProvider.Facebook;

        public async Task<string> LoginAsync(AuthRequest request) {
            var urlQuery = new StringBuilder();
            urlQuery.Append("https://graph.facebook.com/v2.8/oauth/access_token");
            urlQuery.AppendFormat("?client_id={0}", clientId);
            urlQuery.AppendFormat("&client_secret={0}", clientSecret);
            urlQuery.AppendFormat("&redirect_uri={0}", request.redirectUri);
            urlQuery.AppendFormat("&code={0}", request.code);
            
            var tokenResult = await httpClient.GetAsync(urlQuery.ToString());
            if (!tokenResult.IsSuccessStatusCode)
                throw AuthenticationException.InvalidCredential;

            var tokenResponse = await tokenResult.Content.ReadAsAsync<FacebookAccesTokenResponse>();

            var meUrlQuery = new StringBuilder();
            meUrlQuery.Append("https://graph.facebook.com/v2.8/me");
            meUrlQuery.Append("?fields=first_name,last_name,email,picture,gender");
            meUrlQuery.AppendFormat("&access_token={0}", tokenResponse.access_token);


            HttpRequestMessage meRequest = new HttpRequestMessage(HttpMethod.Get, new Uri(meUrlQuery.ToString()));

            var meResult = await httpClient.SendAsync(meRequest);
            if (!meResult.IsSuccessStatusCode)
                throw AuthenticationException.InvalidCredential;

            var facebookUser = await meResult.Content.ReadAsAsync<FacebookMeResponse>();

            var dbUser = userSecurityRepository.GetByEmail(facebookUser.email);
            if (dbUser == null) {
                var userRole = roleRepository.GetByName("User");

                var userSecurity = new UserSecurity(
                    Guid.Empty,
                    facebookUser.email,
                    AuthProvider.Facebook,
                    tokenResponse.access_token,
                    tokenResponse.access_token,
                    Guid.NewGuid().ToString(),
                    null,
                    null,
                    facebookUser.locale == "tr" ? "tr" : "en", // TODO: check language correctly
                    facebookUser.first_name,
                    facebookUser.last_name,
                    facebookUser.picture?.data?.url,
                    facebookUser.gender == "male" ? Gender.Male : Gender.Female,
                    false,
                    true,
                    new List<Role> { userRole }
                );

                userSecurity.CreateToken(abiokaToken, tokenResponse.access_token);

                userSecurityRepository.Add(userSecurity);

                return userSecurity.Token;
            }

            if (dbUser.AuthProvider != AuthProvider.Facebook) {
                throw new DenialException($"UserIsRegisteredFor{dbUser.AuthProvider}", dbUser.Email);
            }

            dbUser.UpdateProviderRefreshToken(tokenResponse.access_token);
            dbUser.CreateToken(abiokaToken, tokenResponse.access_token);
            userSecurityRepository.Update(dbUser);
            return dbUser.Token;
        }

        public async Task<string> RefreshTokenAsync(string refreshToken) {
            var dbUser = userSecurityRepository.GetByRefreshToken(refreshToken);
            if (dbUser == null) {
                throw AuthenticationException.InvalidCredential;
            }

            var urlQuery = new StringBuilder();
            urlQuery.Append("https://graph.facebook.com/v2.8/oauth/access_token");
            urlQuery.Append("?grant_type=fb_exchange_token");
            urlQuery.AppendFormat("&client_id={0}", clientId);
            urlQuery.AppendFormat("&client_secret={0}", clientSecret);
            urlQuery.AppendFormat("&fb_exchange_token={0}", dbUser.ProviderRefreshToken);
            
            var tokenResult = await httpClient.GetAsync(urlQuery.ToString());
            if (!tokenResult.IsSuccessStatusCode)
                throw AuthenticationException.InvalidCredential;

            var tokenResponse = await tokenResult.Content.ReadAsAsync<FacebookAccesTokenResponse>();

            dbUser.CreateToken(abiokaToken, tokenResponse.access_token);
            userSecurityRepository.Update(dbUser);
            return dbUser.Token;
        }

        class FacebookMeResponse
        {
            public string gender { get; set; }

            public string first_name { get; set; }

            public string last_name { get; set; }

            public FacebookPicture picture { get; set; }

            public string email { get; set; }

            public string locale { get; set; }
        }

        class FacebookPicture
        {
            public FacebookPictureData data { get; set; }
        }

        class FacebookPictureData
        {
            public bool is_silhouette { get; set; }

            public string url { get; set; }
        }

        class FacebookAccesTokenResponse
        {
            public string access_token { get; set; }

            public string token_type { get; set; }

            public int expires_in { get; set; }
        }
    }
}
