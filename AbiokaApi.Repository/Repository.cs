using AbiokaApi.Infrastructure.Common.Domain;
using AbiokaApi.Repository.DatabaseObjects;
using AbiokaApi.Repository.Mappings;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.Linq;
using System.Collections.Generic;
using System.Linq;

namespace AbiokaApi.Repository
{
    public class Repository<T, TDBEntity> : IRepository<T>
        where T : IEntity
        where TDBEntity : DBEntity
    {
        protected ISession Session => UnitOfWork.Current.Session;

        public void Add(T entity) {
            var dbObject = DBObjectMapper.FromDomainObject(entity);
            if (dbObject is IDeletableEntity) {
                ((IDeletableEntity)dbObject).IsDeleted = false;
            }
            var id = Session.Save(dbObject);
            var idProperty = entity.GetType().GetProperty("Id");
            if (idProperty != null) {
                idProperty.SetValue(entity, id);
            }
        }

        public void Delete(T entity) {
            var dbEntity = DBObjectMapper.FromDomainObject(entity);
            if (dbEntity is IDeletableEntity) {
                ((IDeletableEntity)dbEntity).IsDeleted = true;
                Session.Merge(dbEntity);
                return;
            }

            Session.Delete(dbEntity);
        }

        public T FindById(object id) {
            var dbEntity = Session.Get<TDBEntity>(id);

            if (dbEntity == null)
                return default(T);

            var result = (T)DBObjectMapper.ToDomainObject(dbEntity);
            return result;
        }

        public virtual IEnumerable<T> GetAll() {
            var list = Query.AsEnumerable();
            var result = DBObjectMapper.ToDomainObjects<T>(list);
            return result;
        }

        public void Update(T entity) {
            var dbObject = DBObjectMapper.FromDomainObject(entity);
            Session.Merge(dbObject);
        }

        public IPage<T> GetPage(PageRequest pageRequest) {
            var query = Session.QueryOver<TDBEntity>()
                .Skip((pageRequest.Page - 1) * pageRequest.Limit)
                .Take(pageRequest.Limit);

            if (!string.IsNullOrWhiteSpace(pageRequest.Order)) {
                query.UnderlyingCriteria.AddOrder(new Order(pageRequest.Order, pageRequest.Ascending));
            }

            var list = query.Future<TDBEntity>().ToList();

            var rowcount = Session.QueryOver<TDBEntity>()
                .Select(Projections.Count(Projections.Id()))
                .FutureValue<int>().Value;


            IPage<T> result = new Page<T>();
            result.Count = rowcount;
            result.Data = DBObjectMapper.ToDomainObjects<T>(list);
            return result;
        }

        protected IQueryable<TDBEntity> Query {
            get {
                var query = Session.Query<TDBEntity>();
                if (typeof(IDeletableEntity).IsAssignableFrom(typeof(TDBEntity))) {
                    query = query.Where(e => !((IDeletableEntity)e).IsDeleted);
                }
                return query;
            }
        }
    }
}
