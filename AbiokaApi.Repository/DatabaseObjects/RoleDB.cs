using System;
using System.Collections.Generic;

namespace AbiokaApi.Repository.DatabaseObjects
{
    public class RoleDB : DeletableEntity
    {
        public RoleDB() {
           // UserRoles = new List<UserRoleDB>();
        }

        public virtual Guid Id { get; set; }

        public virtual string Name { get; set; }

        //public virtual IList<UserRoleDB> UserRoles { get; set; }
    }

    public class UserRoleDB : DBEntity
    {
        public virtual Guid Id { get; set; }

        public virtual Guid UserId { get; set; }

        //public virtual Guid RoleId { get; set; }

        public virtual RoleDB Role { get; set; }
        /*
        public override bool Equals(object obj) {
            var other = obj as UserRoleDB;

            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;

            return UserId == other.UserId && RoleId == other.RoleId;
        }

        public override int GetHashCode() {
            unchecked {
                int hash = GetType().GetHashCode();
                hash = (hash * 31) ^ UserId.GetHashCode();
                hash = (hash * 31) ^ RoleId.GetHashCode();

                return hash;
            }
        }
        */
    }
}
