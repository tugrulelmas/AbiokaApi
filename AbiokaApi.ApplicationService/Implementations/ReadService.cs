using AbiokaApi.ApplicationService.Abstractions;
using AbiokaApi.Infrastructure.Common.Domain;
using AbiokaApi.Infrastructure.Common.Exceptions;
using System.Net;

namespace AbiokaApi.ApplicationService.Implementations
{
    public class ReadService<T> : IReadService<T> where T : IEntity
    {
        protected readonly IRepository<T> repository;

        public ReadService(IRepository<T> repository) {
            this.repository = repository;
        }

        public T Get(object id) => repository.FindById(id);

        public virtual IPage<T> GetWithPage(int page, int limit, string order) {
            var pageRequest = new PageRequest {
                Page = page,
                Limit = limit
            };
            if (!string.IsNullOrWhiteSpace(order)) {
                if (order.StartsWith("-")) {
                    pageRequest.Order = order.Substring(1);
                    pageRequest.Ascending = false;
                }
                else {
                    pageRequest.Order = order;
                    pageRequest.Ascending = true;
                }
            }
            return repository.GetPage(pageRequest);
        }

        protected virtual T GetEntity(object id) {
            var entity = Get(id);
            if (entity == null)
                throw new DenialException(HttpStatusCode.NotFound, "EntityNotFound");

            return entity;
        }
    }
}
