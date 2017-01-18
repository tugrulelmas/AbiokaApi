using System;
using System.Collections.Generic;

namespace AbiokaApi.ApplicationService.DTOs
{
    public class MenuDTO : DTO
    {
        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        /// <value>
        /// The text.
        /// </value>
        public string Text { get; set; }

        /// <summary>
        /// Gets or sets the URL.
        /// </summary>
        /// <value>
        /// The URL.
        /// </value>
        public string Url { get; set; }

        /// <summary>
        /// Gets or sets the children.
        /// </summary>
        /// <value>
        /// The children.
        /// </value>
        public IEnumerable<MenuDTO> Children { get; set; }

        public MenuDTO Parent { get; set; }

        public RoleDTO Role { get; set; }

        public short Order { get; set; }
    }
}
