using System;
using AbiokaApi.Domain;
using AbiokaApi.Domain.Repositories;
using AbiokaApi.Repository.DatabaseObjects;
using System.Linq;
using AbiokaApi.Repository.Mappings;

namespace AbiokaApi.Repository.Repositories
{
    public class RoleRepository : Repository<Role, RoleDB>, IRoleRepository
    {
        public Role GetByName(string name) {
            var roleDB = Query.Where(q => q.Name.ToLowerInvariant() == name.ToLowerInvariant()).FirstOrDefault();
            if (roleDB == null)
                return null;

            return (Role)DBObjectMapper.ToDomainObject(roleDB);
        }
    }
}
