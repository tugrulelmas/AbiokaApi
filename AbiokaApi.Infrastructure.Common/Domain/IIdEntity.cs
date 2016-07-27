namespace AbiokaApi.Infrastructure.Common.Domain
{
    public interface IIdEntity<TId> : IEntity
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        TId Id { get; set; }
    }
}
