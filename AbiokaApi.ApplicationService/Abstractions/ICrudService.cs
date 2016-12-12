using AbiokaApi.Infrastructure.Common.Domain;
using System;

namespace AbiokaApi.ApplicationService.Abstractions
{
    public interface ICrudService<T> : IReadService<T> where T : IEntity
    {
        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        void Delete(object id);

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
    }
}
