using System.Collections.Generic;

namespace AbiokaApi.Infrastructure.Common.Validation
{
    public class ValidationResult
    {
        /// <summary>
        /// Returns true if the validatable object is valid.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is valid; otherwise, <c>false</c>.
        /// </value>
        public bool IsValid { get; set; }

        /// <summary>
        /// Gets or sets the messages.
        /// </summary>
        /// <value>
        /// The messages.
        /// </value>
        public IEnumerable<ValidationMessage> Messages { get; set; }
    }
}
