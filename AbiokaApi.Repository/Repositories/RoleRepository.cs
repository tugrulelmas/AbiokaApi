using AbiokaApi.Domain;
using AbiokaApi.Domain.Repositories;
using AbiokaApi.Repository.DatabaseObjects;
using System;
using System.Linq;

namespace AbiokaApi.Repository.Repositories
{
    public class RoleRepository : Repository<Role>, IRoleRepository
    {
        public void AddToUser(Guid roleId, Guid userId) {
            var userRole = new UserRoleDB {
                Role = new Role(roleId, string.Empty),
                UserId = userId
            };
            Save(userRole);
        }

        public Role GetByName(string name) {
            var result = Query.Where(q => q.Name.ToLowerInvariant() == name.ToLowerInvariant()).FirstOrDefault();
            return result;
        }

        public void RemoveFromUser(Guid roleId, Guid userId) {
            var userRole = GetQuery<UserRoleDB>().Where(ur => ur.UserId == userId && ur.Role.Id == roleId).FirstOrDefault();
            Delete(userRole);
        }
    }
}
