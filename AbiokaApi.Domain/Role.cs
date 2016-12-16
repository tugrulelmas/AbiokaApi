using AbiokaApi.Infrastructure.Common.Domain;
using System;

namespace AbiokaApi.Domain
{
    public class Role : IdEntity<Guid>
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }
    }
}
