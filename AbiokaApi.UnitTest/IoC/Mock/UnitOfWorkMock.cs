using AbiokaApi.Infrastructure.Common.Helper;
using AbiokaApi.Repository;
using Moq;
using NHibernate;

namespace AbiokaApi.UnitTest.IoC.Mock
{
    class UnitOfWorkMock : UnitOfWork
    {
        public readonly Mock<ISessionFactory> SessionFactoryMock;

        public UnitOfWorkMock(Mock<ISessionFactory> sessionFactoryMock, IContextHolder contextHolder)
            : base(sessionFactoryMock.Object, contextHolder) {

            SessionFactoryMock = sessionFactoryMock;
        }

        public static UnitOfWorkMock Create() {
            return new UnitOfWorkMock(new Mock<ISessionFactory>(), ContextHolderMock.Create());
        }
    }
}
