using AbiokaApi.Infrastructure.Common.Domain;
using System;

namespace AbiokaApi.Domain
{
    public class Role : DeletableEntity
    {
        public Role() {

        }

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
        public virtual string Name { get; protected set; }
    }
}
