using AbiokaApi.Infrastructure.Common.Domain;

namespace AbiokaApi.Domain.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        /// <summary>
        /// Count of users.
        /// </summary>
        /// <returns></returns>
        int Count();
    }
}
