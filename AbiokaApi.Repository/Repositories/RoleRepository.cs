using AbiokaApi.Domain;
using AbiokaApi.Domain.Repositories;
using AbiokaApi.Repository.DatabaseObjects;
using AbiokaApi.Repository.Mappings;
using System.Linq;
using System;
using NHibernate.Linq;

namespace AbiokaApi.Repository.Repositories
{
    public class RoleRepository : Repository<Role, RoleDB>, IRoleRepository
    {
        public void AddToUser(Guid roleId, Guid userId) {
            var userRole = new UserRoleDB {
                Role = new RoleDB { Id = roleId },
                UserId = userId
            };
            Session.Save(userRole);
        }

        public Role GetByName(string name) {
            var roleDB = Query.Where(q => q.Name.ToLowerInvariant() == name.ToLowerInvariant()).FirstOrDefault();
            if (roleDB == null)
                return null;

            return (Role)DBObjectMapper.ToDomainObject(roleDB);
        }

        public void RemoveFromUser(Guid roleId, Guid userId) {
            var userRole = Session.Query<UserRoleDB>().Where(ur => ur.UserId == userId && ur.Role.Id == roleId).FirstOrDefault();
            Session.Delete(userRole);
        }
    }
}
