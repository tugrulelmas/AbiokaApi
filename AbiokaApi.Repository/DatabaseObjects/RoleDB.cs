using System;

namespace AbiokaApi.Repository.DatabaseObjects
{
    public class RoleDB : DeletableEntity
    {
        public virtual Guid Id { get; set; }

        public virtual string Name { get; set; }
    }
}
