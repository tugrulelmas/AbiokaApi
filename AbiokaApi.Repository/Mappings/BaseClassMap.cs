using AbiokaApi.Infrastructure.Common.Domain;
using FluentNHibernate.Mapping;

namespace AbiokaApi.Repository.Mappings
{
    internal class BaseClassMap<T> : ClassMap<T> where T : IEntity
    {
        public BaseClassMap() {
            Map(x => x.CreatedDate).Not.Nullable().Not.Update();
            Map(x => x.UpdatedDate).Not.Nullable();
        }
    }
}
