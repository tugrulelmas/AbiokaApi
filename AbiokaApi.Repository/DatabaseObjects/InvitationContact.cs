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
        public virtual string IpAddress { get; set; }

        public override void CopyToDomainObject(IEntity entity) {
            var invitationContact = (InvitationContact)entity;
            invitationContact.Id = Id;
            invitationContact.Name = Name;
            invitationContact.Email = Email;
            invitationContact.Phone = Phone;
            invitationContact.Message = Message;
            invitationContact.IpAddress = IpAddress;
        }

        public override IEntity CreateDomainObject() {
            var entity = new InvitationContact();
            CopyToDomainObject(entity);
            return entity;
        }
    }
}
