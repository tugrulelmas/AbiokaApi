using AbiokaApi.ApplicationService.Messaging;
using AbiokaApi.Domain.Repositories;
using AbiokaApi.Infrastructure.Common.Authentication;
using System;
using System.Threading.Tasks;

namespace AbiokaApi.ApplicationService.Authentication
{
    public class LocalAuthService : IAuthService
    {
        private readonly IUserSecurityRepository userSecurityRepository;
        private readonly IAbiokaToken abiokaToken;

        public LocalAuthService(IUserSecurityRepository userSecurityRepository, IAbiokaToken abiokaToken) {
            this.userSecurityRepository = userSecurityRepository;
            this.abiokaToken = abiokaToken;
        }

        public AuthProvider Provider => AuthProvider.Local;

        public Task<string> LoginAsync(AuthRequest request) {
            var user = userSecurityRepository.GetByEmail(request.Email);
            user.CreateToken(abiokaToken, Guid.NewGuid().ToString());
            userSecurityRepository.Update(user);
            return Task.FromResult(user.Token);
        }

        public Task<string> RefreshTokenAsync(string refreshToken) {
            var user = userSecurityRepository.GetByRefreshToken(refreshToken);
            if (user == null)
                throw AuthenticationException.InvalidCredential;

            user.CreateToken(abiokaToken, Guid.NewGuid().ToString());
            userSecurityRepository.Update(user);
            return Task.FromResult(user.Token);
        }
    }
}
