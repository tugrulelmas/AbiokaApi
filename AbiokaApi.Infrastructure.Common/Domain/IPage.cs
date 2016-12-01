using System.Collections.Generic;

namespace AbiokaApi.Infrastructure.Common.Domain
{
    public interface IPage<T> where T : IEntity
    {
        /// <summary>
        /// Gets or sets the count.
        /// </summary>
        /// <value>
        /// The count.
        /// </value>
        int Count { get; set; }

        /// <summary>
        /// Gets or sets the data.
        /// </summary>
        /// <value>
        /// The data.
        /// </value>
        IEnumerable<T> Data { get; set; }
    }
}
