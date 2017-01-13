using AbiokaApi.ApplicationService.Abstractions;
using AbiokaApi.ApplicationService.DTOs;
using AbiokaApi.Infrastructure.Common.Domain;
using AbiokaApi.Infrastructure.Common.Exceptions;
using System.Collections.Generic;
using System.Net;

namespace AbiokaApi.ApplicationService.Implementations
{
    public class ReadService<TEntity, TDTO> : IReadService<TDTO> where TEntity : class, IEntity where TDTO : DTO
    {
        protected readonly IRepository<TEntity> repository;

        public ReadService(IRepository<TEntity> repository) {
            this.repository = repository;
        }

        public IEnumerable<TDTO> GetAll() {
            var entities = repository.GetAll();
            return DTOMapper.FromDomainObject<TDTO>(entities);
        }

        public TDTO Get(object id) {
            var entity = repository.FindById(id);
            return (TDTO)DTOMapper.FromDomainObject(entity);
        }

        public virtual IPage<TDTO> GetWithPage(int page, int limit, string order) {
            var pageRequest = new PageRequest {
                Page = page,
                Limit = limit
            };
            if (!string.IsNullOrWhiteSpace(order)) {
                if (order.StartsWith("-")) {
                    pageRequest.Order = order.Substring(1);
                    pageRequest.Ascending = false;
                } else {
                    pageRequest.Order = order;
                    pageRequest.Ascending = true;
                }
            }
            var pageResult = repository.GetPage(pageRequest);
            if (pageResult == null)
                return null;

            var result = new Page<TDTO> {
                Count = pageResult.Count,
                Data = DTOMapper.FromDomainObject<TDTO>(pageResult.Data)
            };

            return result;
        }

        protected virtual TEntity GetEntity(object id) {
            var entity = repository.FindById(id);
            if (entity == null)
                throw new DenialException(HttpStatusCode.NotFound, "EntityNotFound");

            return entity;
        }
    }
}
