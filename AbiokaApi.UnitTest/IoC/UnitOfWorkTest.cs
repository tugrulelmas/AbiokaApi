using AbiokaApi.UnitTest.IoC.Mock;
using Moq;
using NHibernate;
using NUnit.Framework;

namespace AbiokaApi.UnitTest.IoC
{
    [TestFixture]
    public class UnitOfWorkTest
    {
        [Test]
        public void UnitOfWork_BeginTransaction() {
            var unitOfWork = UnitOfWorkMock.Create();
            unitOfWork.SessionFactoryMock.Setup(s => s.OpenSession()).Returns(new Mock<ISession>().Object);
            unitOfWork.BeginTransaction();

            unitOfWork.SessionFactoryMock.Verify(m => m.OpenSession(), Times.Once);
        }

        [Test]
        public void UnitOfWork_IsInTransaction_True() {
            var unitOfWork = UnitOfWorkMock.Create();
            var sessionMock = new Mock<ISession>();
            var transactionMock = new Mock<ITransaction>();
            transactionMock.Setup(t => t.IsActive).Returns(true);
            sessionMock.Setup(s => s.BeginTransaction()).Returns(transactionMock.Object);
            unitOfWork.SessionFactoryMock.Setup(s => s.OpenSession()).Returns(sessionMock.Object);
            unitOfWork.BeginTransaction();

            Assert.IsTrue(unitOfWork.IsInTransaction);
        }
    }
}
