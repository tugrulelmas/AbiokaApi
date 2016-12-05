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
    }
}