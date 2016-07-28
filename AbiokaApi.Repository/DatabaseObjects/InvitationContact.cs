using AbiokaApi.Domain;
using AbiokaApi.Infrastructure.Common.Domain;

namespace AbiokaApi.Repository.DatabaseObjects
{
    internal class InvitationContactDB : DBEntity
    {
        public virtual int Id { get; set; }

        public virtual string Name { get; set; }

        public virtual string Email { get; set; }

        public virtual string Phone { get; set; }

        public virtual string Message { get; set; }

        public override IEntity ToDomainObject() {
            var entity = new InvitationContact()
            {
                Id = Id,
                Name = Name,
                Email = Email,
                Phone = Phone,
                Message = Message
            };

            return entity;
        }
    }
}
