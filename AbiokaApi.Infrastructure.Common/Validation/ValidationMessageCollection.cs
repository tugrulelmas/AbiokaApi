using System.Collections;
using System.Collections.Generic;

namespace AbiokaApi.Infrastructure.Common.Validation
{
    public class ValidationMessageCollection : IEnumerable<ValidationMessage>
    {
        private List<ValidationMessage> validationMessages;

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationMessageCollection"/> class.
        /// </summary>
        public ValidationMessageCollection() {
            validationMessages = new List<ValidationMessage>();
        }

        /// <summary>
        /// Adds the specified validation message.
        /// </summary>
        /// <param name="validationMessage">The validation message.</param>
        public void Add(ValidationMessage validationMessage) {
            validationMessages.Add(validationMessage);
        }

        /// <summary>
        /// Adds the range.
        /// </summary>
        /// <param name="collection">The collection.</param>
        public void AddRange(IEnumerable<ValidationMessage> collection) {
            validationMessages.AddRange(collection);
        }

        /// <summary>
        /// Adds the empty message.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="name">The name.</param>
        public void AddEmptyMessage(string value, string name) {
            if (string.IsNullOrWhiteSpace(value))
            {
                validationMessages.Add(new ValidationMessage()
                {
                    ErrorCode = ValidationCode.EmptyProperty,
                    Args = new List<ValidationArg> { new ValidationArg { Name = name, IsLocalizable = true } }
                });
            }
        }

        /// <summary>
        /// To the validation result.
        /// </summary>
        /// <returns></returns>
        public ValidationResult ToValidationResult() {
            return new ValidationResult()
            {
                IsValid = validationMessages.Count == 0,
                Messages = validationMessages
            };
        }

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>
        /// An enumerator that can be used to iterate through the collection.
        /// </returns>
        public IEnumerator<ValidationMessage> GetEnumerator() {
            return validationMessages.GetEnumerator();
        }

        /// <summary>
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>
        /// An <see cref="T:System.Collections.IEnumerator" /> object that can be used to iterate through the collection.
        /// </returns>
        IEnumerator IEnumerable.GetEnumerator() {
            return GetEnumerator();
        }
    }
}
