using AbiokaApi.Infrastructure.Common.Domain;
using System;

namespace AbiokaApi.Domain
{
    public class Role : IdEntity<Guid>
    {
        public Role(Guid id, string name) {
            Id = id;
            Name = name;
        }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; protected set; }
    }
}
