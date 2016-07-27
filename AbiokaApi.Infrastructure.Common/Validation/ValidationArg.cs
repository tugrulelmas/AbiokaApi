namespace AbiokaApi.Infrastructure.Common.Validation
{
    public class ValidationArg
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is localizable.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is localizable; otherwise, <c>false</c>.
        /// </value>
        public bool IsLocalizable { get; set; }
    }
}
