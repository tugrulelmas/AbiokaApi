using AbiokaApi.Infrastructure.Common.Domain;
using AbiokaApi.Infrastructure.Common.Validation;
using System;

namespace AbiokaApi.Domain
{
    public class Role : IdEntity<Guid>
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        public override ValidationResult Validate(ActionType actionType) {
            var collection = new ValidationMessageCollection();

            if (actionType != ActionType.Delete)
            {
                collection.AddEmptyMessage(Name, "Name");
            }

            return collection.ToValidationResult();
        }
    }
}
