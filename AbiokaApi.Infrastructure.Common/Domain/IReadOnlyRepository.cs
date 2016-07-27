using System.Collections.Generic;

namespace AbiokaApi.Infrastructure.Common.Domain
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IReadOnlyRepository<T> where T : IEntity
    {
        /// <summary>
        /// Finds the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        T FindById(object id);

        /// <summary>
        /// Gets all.
        /// </summary>
        /// <returns></returns>
        IEnumerable<T> GetAll();
    }
}
