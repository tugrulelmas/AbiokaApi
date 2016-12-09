using AbiokaApi.Infrastructure.Common.Domain;
using System;

namespace AbiokaApi.ApplicationService.Abstractions
{
    public interface ICrudService<T> : IService where T : IEntity
    {
        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        void Delete(Guid id);

        /// <summary>
        /// Updates the specified entiy.
        /// </summary>
        /// <param name="entiy">The entiy.</param>
        void Update(T entiy);

        /// <summary>
        /// Adds the specified entiy.
        /// </summary>
        /// <param name="entiy">The entiy.</param>
        void Add(T entiy);

        /// <summary>
        /// Gets the with page.
        /// </summary>
        /// <param name="page">The page.</param>
        /// <param name="limit">The limit.</param>
        /// <param name="order">The order.</param>
        /// <returns></returns>
        IPage<T> GetWithPage(int page, int limit, string order);
    }
}
