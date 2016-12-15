using AbiokaApi.Domain;
using AbiokaApi.Domain.Repositories;
using AbiokaApi.Repository.DatabaseObjects;
using AbiokaApi.Repository.Mappings;
using System.Linq;
using NHibernate.Linq;
using System.Collections.Generic;

namespace AbiokaApi.Repository.Repositories
{
    public class UserSecurityRepository : Repository<UserSecurity, UserSecurityDB>, IUserSecurityRepository
    {
        public override void Add(UserSecurity entity) {
            base.Add(entity);
            if (entity.Roles != null && entity.Roles.Count() > 0) {
                foreach (var roleItem in entity.Roles) {
                    var userRole = new UserRoleDB {
                        Role = new RoleDB { Id = roleItem.Id },
                        UserId = entity.Id
                    };
                    Session.Save(userRole);
                }
            }
        }

        public UserSecurity GetByEmail(string email) {
            var dbUser = Query.Where(u => u.Email.ToLowerInvariant() == email.ToLowerInvariant()).FirstOrDefault();
            if (dbUser == null)
                return null;

            var result = (UserSecurity)DBObjectMapper.ToDomainObject(dbUser);
            var userRoles = Session.Query<UserRoleDB>().Where(ur => ur.UserId == result.Id);
            if (userRoles != null && userRoles.Count() > 0) {
                var roles = new List<Role>();
                foreach (var userRoleItem in userRoles) {
                    roles.Add(new Role { Id = userRoleItem.Role.Id, Name = userRoleItem.Role.Name });
                }
                result.Roles = roles;
            }
            return result;
        }
    }
}