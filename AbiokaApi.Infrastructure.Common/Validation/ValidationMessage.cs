using System.Collections.Generic;

namespace AbiokaApi.Infrastructure.Common.Validation
{
    public class ValidationMessage
    {
        /// <summary>
        /// Gets or sets the error code.
        /// </summary>
        /// <value>
        /// The error code.
        /// </value>
        public string ErrorCode { get; set; }

        /// <summary>
        /// Gets or sets the arguments.
        /// </summary>
        /// <value>
        /// The arguments.
        /// </value>
        public IEnumerable<ValidationArg> Args { get; set; }
    }
}
