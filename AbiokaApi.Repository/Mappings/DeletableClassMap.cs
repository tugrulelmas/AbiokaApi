using AbiokaApi.Repository.DatabaseObjects;
using FluentNHibernate.Mapping;

namespace AbiokaApi.Repository.Mappings
{
    internal class DeletableClassMap<T> : ClassMap<T> where T : IDeletableEntity
    {
        public DeletableClassMap() {
            Map(x => x.IsDeleted);
        }
    }
}
