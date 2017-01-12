using AbiokaApi.Domain;
using AbiokaApi.Domain.Repositories;
using AbiokaApi.Infrastructure.Common.Helper;
using AbiokaApi.Repository.DatabaseObjects;
using NHibernate.Linq;
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

            if (userRoles.IsNullOrEmpty())
                return user;

            var roles = new List<Role>();
            foreach (var userRoleItem in userRoles) {
                roles.Add(new Role(userRoleItem.Role.Id, userRoleItem.Role.Name));
            }
            return new User(user.Id, user.Email, roles);
        }
    }
}
