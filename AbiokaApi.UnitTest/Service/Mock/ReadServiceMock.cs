using System;
using System.Collections.Generic;
using AbiokaApi.ApplicationService.Implementations;
using AbiokaApi.Infrastructure.Common.Domain;
using Moq;
using AbiokaApi.ApplicationService.DTOs;

namespace AbiokaApi.UnitTest.Service.Mock
{
    class ReadServiceMock : ReadService<MockEntity, MockEntityDTO>
    {
        public readonly Mock<IRepository<MockEntity>> RepositoryMock;

        public ReadServiceMock(Mock<IRepository<MockEntity>> repository) : base(repository.Object) {
            RepositoryMock = repository;
        }

        public new MockEntity GetEntity(object id) => base.GetEntity(id);

        public static ReadServiceMock Create() => new ReadServiceMock(new Mock<IRepository<MockEntity>>());
    }

    public class MockEntity : IEntity
    {
        public DateTime CreatedDate { get; set; }

        public DateTime UpdatedDate { get; set; }

        public IEnumerable<IEvent> Events {
            get {
                throw new NotImplementedException();
            }
        }
    }

    public class MockEntityDTO : DTO
    {

    }
}
