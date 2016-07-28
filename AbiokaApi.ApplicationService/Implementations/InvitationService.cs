﻿using AbiokaApi.ApplicationService.Abstractions;
using AbiokaApi.ApplicationService.Messaging;
using AbiokaApi.Domain;
using AbiokaApi.Domain.Repositories;

namespace AbiokaApi.ApplicationService.Implementations
{
    public class InvitationService : IInvitationService
    {
        private readonly IInvitationContactRepository repository;

        public InvitationService(IInvitationContactRepository repository) {
            this.repository = repository;
        }

        public SaveInvitaionContactResponse SaveInvitaionContact(SaveInvitaionContactRequest request) {
            var aa = repository.FindById(1);
            var invitationContact = new InvitationContact
            {
                Email = request.Email,
                Message = request.Message,
                Name = request.Name,
                Phone = request.Phone
            };

            repository.Add(invitationContact);
            var result = new SaveInvitaionContactResponse
            {
                Id = invitationContact.Id
            };

            return result;
        }
    }
}
