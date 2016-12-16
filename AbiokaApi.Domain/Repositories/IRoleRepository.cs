using AbiokaApi.Infrastructure.Common.Domain;

namespace AbiokaApi.Domain.Repositories
{
    public interface IRoleRepository : IRepository<Role>
    {
        /// <summary>
        /// Gets the role by name.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        Role GetByName(string name);
    }
}
