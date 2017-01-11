using AbiokaApi.Infrastructure.Common.Domain;
using System;

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

        /// <summary>
        /// Adds to user.
        /// </summary>
        /// <param name="roleId">The role identifier.</param>
        /// <param name="userId">The user identifier.</param>
        void AddToUser(Guid roleId, Guid userId);

        /// <summary>
        /// Removes from user.
        /// </summary>
        /// <param name="roleId">The role identifier.</param>
        /// <param name="userId">The user identifier.</param>
        void RemoveFromUser(Guid roleId, Guid userId);
    }
}
