using AbiokaApi.Infrastructure.Common.Domain;
using System;
using System.Collections.Generic;

namespace AbiokaApi.Domain
{
    public class Menu : IdEntity<Guid>
    {
        public Menu() {

        }

        public Menu(Guid id, string text, string url, short order, Menu parent, IEnumerable<Menu> children) {
            Id = id;
            Text = text;
            Url = url;
            Order = order;
            Parent = parent;
            Children = children;
        }

        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        /// <value>
        /// The text.
        /// </value>
        public virtual string Text { get; protected set; }

        /// <summary>
        /// Gets or sets the URL.
        /// </summary>
        /// <value>
        /// The URL.
        /// </value>
        public virtual string Url { get; protected set; }

        public virtual short Order { get; protected set; }

        /// <summary>
        /// Gets or sets the parent.
        /// </summary>
        /// <value>
        /// The parent.
        /// </value>
        public virtual Menu Parent { get; protected set; }

        /// <summary>
        /// Gets or sets the children.
        /// </summary>
        /// <value>
        /// The children.
        /// </value>
        public virtual IEnumerable<Menu> Children { get; protected set; }
    }
}
