namespace AbiokaApi.Repository.DatabaseObjects
{
    internal interface IDeletableEntity
    {
        /// <summary>
        /// Gets or sets a value indicating whether this instance is deleted.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is deleted; otherwise, <c>false</c>.
        /// </value>
        bool IsDeleted { get; set; }
    }

    internal abstract class DeletableEntity : DBEntity, IDeletableEntity
    {
        public virtual bool IsDeleted { get; set; }
    }
}
