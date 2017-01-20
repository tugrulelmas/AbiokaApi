using AbiokaApi.ApplicationService.Abstractions;
using AbiokaApi.ApplicationService.Messaging;
using AbiokaApi.Infrastructure.Common.Authentication;
using System.Threading.Tasks;

namespace AbiokaApi.ApplicationService.Authentication
{
    public interface IAuthService : IService
    {
        Task<string> LoginAsync(AuthRequest request);

        Task<string> RefreshTokenAsync(string refreshToken);

        AuthProvider Provider { get; }
    }
}
