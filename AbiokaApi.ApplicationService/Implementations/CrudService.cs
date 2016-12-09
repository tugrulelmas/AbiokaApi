using AbiokaApi.ApplicationService.Abstractions;
using AbiokaApi.Infrastructure.Common.Domain;
using AbiokaApi.Infrastructure.Common.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AbiokaApi.ApplicationService.Implementations
{
    public class CrudService<T> : ICrudService<T> where T : IIdEntity<Guid>
    {
        private readonly IRepository<T> repository;

        public CrudService(IRepository<T> repository) {
            this.repository = repository;
        }

        public virtual void Add(T entiy) {
            repository.Add(entiy);
        }

        public virtual void Delete(Guid id) {
            var entity = GetEntity(id);
            repository.Delete(entity);
        }

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

        public virtual void Update(T entiy) {
            GetEntity(entiy.Id);

            repository.Update(entiy);
        }

        protected virtual T GetEntity(Guid id) {
            var user = repository.FindById(id);
            if (user == null)
                throw new DenialException(HttpStatusCode.NotFound, "EntityNotFound");

            return user;
        }
    }
}
