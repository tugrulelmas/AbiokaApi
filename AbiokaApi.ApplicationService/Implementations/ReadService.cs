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
        protected readonly IDTOMapper dtoMapper;

        public ReadService(IRepository<TEntity> repository, IDTOMapper dtoMapper) {
            this.repository = repository;
            this.dtoMapper = dtoMapper;
        }

        public virtual IEnumerable<TDTO> GetAll() {
            var entities = repository.GetAll();
            return dtoMapper.FromDomainObject<TDTO>(entities);
        }

        public virtual TDTO Get(object id) {
            var entity = repository.FindById(id);
            return (TDTO)dtoMapper.FromDomainObject(entity);
        }

        public virtual IPage<TDTO> GetWithPage(int page, int limit, string order) {
            var pageRequest = new PageRequest {
                Page = page,
                Limit = limit,
                Order = order
            };
            var pageResult = repository.GetPage(pageRequest);
            if (pageResult == null)
                return null;

            var result = new Page<TDTO> {
                Count = pageResult.Count,
                Data = dtoMapper.FromDomainObject<TDTO>(pageResult.Data)
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
