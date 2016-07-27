using AbiokaApi.Infrastructure.Common.Domain;
using AbiokaApi.Infrastructure.Common.Validation;

namespace AbiokaApi.Domain
{
    public class InvitationContact : IdEntity<int>
    {
        public virtual string Name { get; set; }

        public virtual string Email { get; set; }

        public virtual string Phone { get; set; }

        public virtual string Message { get; set; }

        public override ValidationResult Validate(ActionType actionType) {
            var collection = new ValidationMessageCollection();

            if (actionType != ActionType.Delete)
            {
                collection.AddEmptyMessage(Name, "Name");
                collection.AddEmptyMessage(Email, "Email");
                collection.AddEmptyMessage(Message, "Message");
            }

            return collection.ToValidationResult();
        }
    }
}
