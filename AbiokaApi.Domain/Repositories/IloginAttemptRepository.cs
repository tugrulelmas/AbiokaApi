using AbiokaApi.Infrastructure.Common.Domain;

namespace AbiokaApi.Domain.Repositories
{
    public interface ILoginAttemptRepository : IReadOnlyRepository<LoginAttempt>
    {
        /// <summary>
        /// Adds the specified login attempt.
        /// </summary>
        /// <param name="loginAttempt">The login attempt.</param>
        void Add(LoginAttempt loginAttempt);
    }
}
