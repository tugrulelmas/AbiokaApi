using AbiokaApi.Domain;
using AbiokaApi.Infrastructure.Common.Domain;
using System;

namespace AbiokaApi.Repository.DatabaseObjects
{
    public class UserRoleDB : IdEntity<Guid>
    {
        public virtual Guid UserId { get; set; }

        public virtual Role Role { get; set; }
    }
}
