using AbiokaApi.Infrastructure.Common.Domain;

namespace AbiokaApi.Repository.Mappings
{
    public class DeletableClassMap<T> : BaseClassMap<T> where T : IDeletableEntity
    {
        public DeletableClassMap() {
            Map(x => x.IsDeleted);
        }
    }
}
