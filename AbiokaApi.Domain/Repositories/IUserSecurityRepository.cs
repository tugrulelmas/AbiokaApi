using AbiokaApi.Infrastructure.Common.Domain;

namespace AbiokaApi.Domain.Repositories
{
    public interface IUserSecurityRepository : IRepository<UserSecurity>
    {
        /// <summary>
        /// Gets the by email.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <returns></returns>
        UserSecurity GetByEmail(string email);

        /// <summary>
        /// Gets the by refresh token.
        /// </summary>
        /// <param name="refreshToken">The refresh token.</param>
        /// <returns></returns>
        UserSecurity GetByRefreshToken(string refreshToken);
    }
}