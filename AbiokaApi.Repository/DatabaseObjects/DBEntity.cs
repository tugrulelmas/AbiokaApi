using AbiokaApi.Infrastructure.Common.Domain;

namespace AbiokaApi.Repository.DatabaseObjects
{
    internal abstract class DBEntity
    {
        public abstract IEntity ToDomainObject();
    }
}
