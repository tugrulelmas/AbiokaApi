using AbiokaApi.ApplicationService.Abstractions;
using AbiokaApi.Infrastructure.Common.Domain;

namespace AbiokaApi.ApplicationService.Implementations
{
    public class CrudService<T> : ReadService<T>, ICrudService<T> where T : IEntity
    {
        public CrudService(IRepository<T> repository)
            : base(repository) {
        }

        public virtual void Add(T entiy) {
            repository.Add(entiy);
        }

        public virtual void Delete(object id) {
            var entity = GetEntity(id);
            repository.Delete(entity);
        }

        public virtual void Update(T entiy) {
            repository.Update(entiy);
        }
    }
}
