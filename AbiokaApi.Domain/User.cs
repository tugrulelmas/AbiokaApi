using AbiokaApi.Infrastructure.Common.Domain;
using System;
using System.Collections.Generic;

namespace AbiokaApi.Domain
{
    public class User : IdEntity<Guid>
    {
        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        /// <value>
        /// The email.
        /// </value>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the roles.
        /// </summary>
        /// <value>
        /// The roles.
        /// </value>
        public IEnumerable<Role> Roles { get; set; }
    }
}
