using AbiokaApi.Domain;
using AbiokaApi.Domain.Repositories;
using AbiokaApi.Repository.DatabaseObjects;
using NHibernate.Linq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AbiokaApi.Repository.Repositories
{
    public class UserRepository : Repository<User, UserDB>, IUserRepository
    {
        public int Count() {
            var result = Query.Count();
            return result;
        }

        public override User FindById(object id) {
            var user = base.FindById(id);
            var userRoles = Session.Query<UserRoleDB>().Where(ur => ur.UserId == user.Id).ToList();
            if (userRoles != null && userRoles.Count() > 0) {
                var roles = new List<Role>();
                foreach (var userRoleItem in userRoles) {
                    roles.Add(new Role { Id = userRoleItem.Role.Id, Name = userRoleItem.Role.Name });
                }
                user.Roles = roles;
            }
            return user;
        }

        public override void Update(User entity) {
            var userRoles = Session.Query<UserRoleDB>().Where(ur => ur.UserId == entity.Id).ToList();
            IEnumerable<Guid> insertedRoles;
            if (userRoles != null && userRoles.Count > 0) {
                var deletedRoles = userRoles.Where(ur => !entity.Roles.Select(r => r.Id).Contains(ur.Role.Id));
                foreach (var roleItem in deletedRoles) {
                    Session.Delete(roleItem);
                }
                insertedRoles = entity.Roles.Select(r => r.Id).Where(ur => !userRoles.Select(r => r.Role.Id).Contains(ur));
            }
            else {
                insertedRoles = entity.Roles.Select(r => r.Id);
            }

            foreach (var roleItem in insertedRoles) {
                var userRole = new UserRoleDB {
                    Role = new RoleDB { Id = roleItem },
                    //RoleId = roleItem,
                    UserId = entity.Id
                };
                Session.Save(userRole);
            }
            base.Update(entity);
        }
    }
}
