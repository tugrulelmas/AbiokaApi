using AbiokaApi.Infrastructure.Common.Authentication;

namespace AbiokaApi.Infrastructure.Common.Helper
{
    public interface ICurrentContext
    {
        /// <summary>
        /// Gets the current.
        /// </summary>
        /// <value>
        /// The current.
        /// </value>
        ICurrentContext Current { get; }

        /// <summary>
        /// Gets or sets the principal.
        /// </summary>
        /// <value>
        /// The principal.
        /// </value>
        ICustomPrincipal Principal { get; set; }

        /// <summary>
        /// Gets or sets the type of the action.
        /// </summary>
        /// <value>
        /// The type of the action.
        /// </value>
        ActionType ActionType { get; set; }

        /// <summary>
        /// Gets or sets the ip.
        /// </summary>
        /// <value>
        /// The ip.
        /// </value>
        string IP { get; set; }
    }
}
