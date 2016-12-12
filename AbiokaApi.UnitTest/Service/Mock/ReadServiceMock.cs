using AbiokaApi.ApplicationService.Implementations;
using AbiokaApi.Infrastructure.Common.Domain;
using Moq;

namespace AbiokaApi.UnitTest.Service.Mock
{
    class ReadServiceMock : ReadService<MockEntity>
    {
        public readonly Mock<IRepository<MockEntity>> RepositoryMock;

        public ReadServiceMock(Mock<IRepository<MockEntity>> repository) : base(repository.Object) {
            RepositoryMock = repository;
        }

        public new MockEntity GetEntity(object id) => base.GetEntity(id);

        public static ReadServiceMock Create() => new ReadServiceMock(new Mock<IRepository<MockEntity>>());
    }

    public class MockEntity : IEntity { }
}
