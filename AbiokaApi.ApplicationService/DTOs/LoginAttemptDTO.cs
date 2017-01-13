using AbiokaApi.Domain;
using System;

namespace AbiokaApi.ApplicationService.DTOs
{
   public class LoginAttemptDTO : DTO
    {
        /// <summary>
        /// Gets or sets the user.
        /// </summary>
        /// <value>
        /// The user.
        /// </value>
        public UserDTO User { get; set; }

        /// <summary>
        /// Gets or sets the ip.
        /// </summary>
        /// <value>
        /// The ip.
        /// </value>
        public string IP { get; set; }

        /// <summary>
        /// Gets or sets the date.
        /// </summary>
        /// <value>
        /// The date.
        /// </value>
        public DateTime Date { get; set; }

        /// <summary>
        /// Gets or sets the login result.
        /// </summary>
        /// <value>
        /// The login result.
        /// </value>
        public string LoginResult { get; set; }
    }
}
