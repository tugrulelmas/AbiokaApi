namespace AbiokaApi.ApplicationService.Messaging
{
    public class SaveInvitaionContactRequest : ServiceRequestBase
    {
        public string Name { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public string Message { get; set; }

        public string IpAddress { get; set; }
    }
}
