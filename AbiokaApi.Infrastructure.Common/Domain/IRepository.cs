namespace AbiokaApi.Infrastructure.Common.Domain
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <seealso cref="AbiokaApi.Infrastructure.Common.Domain.IReadOnlyRepository{T}" />
    public interface IRepository<T> : IReadOnlyRepository<T> where T : IEntity
    {
        /// <summary>
        /// Adds the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        void Add(T entity);

        /// <summary>
        /// Updates the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        void Update(T entity);

        /// <summary>
        /// Deletes the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        void Delete(T entity);
    }
}
