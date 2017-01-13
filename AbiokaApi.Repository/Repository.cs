using AbiokaApi.Infrastructure.Common.Domain;
using AbiokaApi.Infrastructure.Common.Helper;
using AbiokaApi.Infrastructure.Common.IoC;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace AbiokaApi.Repository
{
    public class Repository<T> : IRepository<T>
        where T : class, IEntity
    {
        private ISession Session => UnitOfWork.Current.Session;

        public virtual void Add(T entity) {
            Add(Session, entity);
        }

        protected void Add(ISession session, T entity) {
            Save(session, entity);
        }

        public virtual void Delete(T entity) {
            Delete((IEntity)entity);
        }

        public virtual T FindById(object id) {
            var result = Session.Get<T>(id);
            return result;
        }

        public virtual IEnumerable<T> GetAll() {
            var result = Query.ToList();
            return result;
        }

        public virtual void Update(T entity) {
            Update((IEntity)entity);
        }

        protected void Delete(IEntity entity) {
            if (entity is IDeletableEntity) {
                ((IDeletableEntity)entity).IsDeleted = true;
                Update(entity);
                return;
            }

            Session.Delete(entity);

            DispatchEvents(entity);
        }

        protected void Update(IEntity entity) {
            entity.UpdatedDate = DateTime.UtcNow;
            Session.Merge(entity);

            DispatchEvents(entity);
        }

        protected void Save(IEntity entity) {
            Save(Session, entity);
        }

        protected void Save(ISession session, IEntity entity) {
            if (entity is IDeletableEntity) {
                ((IDeletableEntity)entity).IsDeleted = false;
            }

            entity.CreatedDate = DateTime.UtcNow;
            entity.UpdatedDate = DateTime.UtcNow;

            session.Save(entity);

            DispatchEvents(entity);
        }

        public virtual IPage<T> GetPage(PageRequest pageRequest) => GetPage(pageRequest, null);

        protected IPage<T> GetPage(PageRequest pageRequest, Expression<Func<T, bool>> filter) {
            var queryOver = Session.QueryOver<T>();
            var rowCountQueryOver = Session.QueryOver<T>();

            if (typeof(IDeletableEntity).IsAssignableFrom(typeof(T))) {
                queryOver = queryOver.Where(e => !((IDeletableEntity)e).IsDeleted);
                rowCountQueryOver = rowCountQueryOver.Where(e => !((IDeletableEntity)e).IsDeleted);
            }

            if (filter != null) {
                queryOver = queryOver.Where(filter);
                rowCountQueryOver = rowCountQueryOver.Where(filter);
            }

            var query = queryOver.Skip((pageRequest.Page - 1) * pageRequest.Limit)
                .Take(pageRequest.Limit);

            if (!string.IsNullOrWhiteSpace(pageRequest.Order)) {
                query.UnderlyingCriteria.AddOrder(new Order(pageRequest.Order, pageRequest.Ascending));
            }

            var list = query.Future<T>().ToList();

            var rowcount = rowCountQueryOver.Select(Projections.Count(Projections.Id()))
                .FutureValue<int>().Value;

            IPage<T> result = new Page<T>();
            result.Count = rowcount;
            result.Data = list;
            return result;
        }

        protected IQueryable<T> Query => GetQuery<T>();

        protected IQueryable<TEntity> GetQuery<TEntity>() {
            var query = Session.Query<TEntity>();
            if (typeof(IDeletableEntity).IsAssignableFrom(typeof(TEntity))) {
                query = query.Where(e => !((IDeletableEntity)e).IsDeleted);
            }
            return query;
        }

        protected void DispatchEvents(IEntity entity) {
            if (entity.Events.IsNullOrEmpty())
                return;

            var eventDispatcher = DependencyContainer.Container.Resolve<IEventDispatcher>();
            eventDispatcher.Dispatch(entity.Events.ToArray());
        }
    }
}
