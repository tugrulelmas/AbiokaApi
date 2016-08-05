using AbiokaApi.Infrastructure.Common.Domain;
using AbiokaApi.Repository.DatabaseObjects;
using AbiokaApi.Repository.Mappings;
using NHibernate;
using NHibernate.Linq;
using System.Collections.Generic;
using System.Linq;

namespace AbiokaApi.Repository
{
    internal class Repository<T, TDBEntity> : IRepository<T>
        where T : IEntity
        where TDBEntity : DBEntity
    {
        protected ISession Session { get { return UnitOfWork.Current.Session; } }

        public void Add(T entity) {
            var dbObject = DBObjectMapper.FromDomainObject(entity);
            var id = Session.Save(dbObject);
            var idProperty = dbObject.GetType().GetProperty("Id");
            if (idProperty != null)
            {
                idProperty.SetValue(dbObject, id);
            }

            dbObject.CopyToDomainObject(entity);
        }

        public void Delete(T entity) {
            Session.Delete(entity);
        }

        public T FindById(object id) {
            var dbEntity = Session.Get<TDBEntity>(id);

            if (dbEntity == null)
                return default(T);
            
            var result = (T)dbEntity.CreateDomainObject();
            return result;
        }

        public virtual IEnumerable<T> GetAll() {
            var list = Session.Query<TDBEntity>().AsEnumerable();

            foreach (var item in list)
            {
                T result = default(T);
                item.CopyToDomainObject(result);
                yield return result;
            }
        }

        public void Update(T entity) {
            var dbObject = DBObjectMapper.FromDomainObject(entity);
            Session.Merge(dbObject);
        }

        protected IQueryable<TDBEntity> Query { get { return Session.Query<TDBEntity>(); } }
    }
}
