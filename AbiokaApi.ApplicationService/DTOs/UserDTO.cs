using System.Collections.Generic;

namespace AbiokaApi.ApplicationService.DTOs
{
    public class UserDTO : DTO
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
        public IEnumerable<RoleDTO> Roles { get; set; }
    }
}
