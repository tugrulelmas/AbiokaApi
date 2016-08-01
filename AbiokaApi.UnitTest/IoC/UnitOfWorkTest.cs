using AbiokaApi.Repository;
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
        public void UnitOfWork_CurrentIsNotNull() {
            var unitOfWork = UnitOfWorkMock.Create();

            Assert.AreEqual(UnitOfWork.Current, unitOfWork);
        }

        [Test]
        public void UnitOfWork_BeginTransaction() {
            var unitOfWork = UnitOfWorkMock.Create();
            unitOfWork.SessionFactoryMock.Setup(s => s.OpenSession()).Returns(new Mock<ISession>().Object);
            unitOfWork.BeginTransaction();

            unitOfWork.SessionFactoryMock.Verify(m => m.OpenSession(), Times.Once);
        }
    }
}
