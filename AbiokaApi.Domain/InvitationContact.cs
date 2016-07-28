using AbiokaApi.Infrastructure.Common.Domain;
using AbiokaApi.Infrastructure.Common.Validation;

namespace AbiokaApi.Domain
{
    public class InvitationContact : IdEntity<int>
    {
        public string Name { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public string Message { get; set; }

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
