using AbiokaApi.Domain;
using AbiokaApi.Domain.Repositories;
using AbiokaApi.Infrastructure.Common.Helper;
using AbiokaApi.Repository.DatabaseObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace AbiokaApi.Repository.Repositories
{
    public class UserSecurityRepository : Repository<UserSecurity>, IUserSecurityRepository
    {
        public UserSecurity GetByEmail(string email) => GetUser(u => u.Email.ToLowerInvariant() == email.ToLowerInvariant());

        public UserSecurity GetByRefreshToken(string refreshToken) => GetUser(u => u.RefreshToken == refreshToken);

        private UserSecurity GetUser(Expression<Func<UserSecurity, bool>> filter) {
            var result = Query().Where(filter).FirstOrDefault();
            if (result == null)
                return null;
            
            var userRoles = GetQuery<UserRoleDB>().Where(ur => ur.UserId == result.Id);

            if (userRoles.IsNullOrEmpty())
                return result;

            var roles = new List<Role>();
            foreach (var userRoleItem in userRoles) {
                roles.Add(new Role(userRoleItem.Role.Id, userRoleItem.Role.Name));
            }
            result.SetRoles(roles);
            result.ClearEvents();
            return result;
        }
    }
}