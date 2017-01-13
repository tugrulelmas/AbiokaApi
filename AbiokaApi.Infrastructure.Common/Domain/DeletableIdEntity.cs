using System;

namespace AbiokaApi.Infrastructure.Common.Domain
{
    public abstract class DeletableIdEntity<IdType> : IdEntity<IdType>, IDeletableEntity
    {
        public virtual bool IsDeleted { get; set; }
    }
}
