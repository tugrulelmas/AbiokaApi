namespace AbiokaApi.Repository.DatabaseObjects
{
    public class InvitationContactDB : DBEntity
    {
        public virtual int Id { get; set; }

        public virtual string Name { get; set; }

        public virtual string Email { get; set; }

        public virtual string Phone { get; set; }

        public virtual string Message { get; set; }

        public virtual string IpAddress { get; set; }
    }
}
