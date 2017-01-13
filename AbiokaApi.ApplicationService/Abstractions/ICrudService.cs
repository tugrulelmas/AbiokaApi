using AbiokaApi.ApplicationService.DTOs;

namespace AbiokaApi.ApplicationService.Abstractions
{
    public interface ICrudService<T> : IReadService<T> where T : DTO
    {
        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        void Delete(object id);

        /// <summary>
        /// Updates the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        void Update(T entity);

        /// <summary>
        /// Adds the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        void Add(T entity);
    }
}
