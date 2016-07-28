using AbiokaApi.ApplicationService.Messaging;

namespace AbiokaApi.ApplicationService.Abstractions
{
    public interface IInvitationService : IService
    {
        SaveInvitaionContactResponse SaveInvitaionContact(SaveInvitaionContactRequest request);
    }
}
