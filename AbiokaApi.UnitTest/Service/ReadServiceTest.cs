using AbiokaApi.Infrastructure.Common.Domain;
using AbiokaApi.Infrastructure.Common.Exceptions;
using AbiokaApi.UnitTest.Service.Mock;
using Moq;
using NUnit.Framework;
using System;
using System.Net;

namespace AbiokaApi.UnitTest.Service
{
    [TestFixture]
    public class ReadServiceTest
    {
        [Test]
        public void GetWithPage_Order_Ascending() {
            var service = ReadServiceMock.Create();
            var order = "Email";
            var page = 1;
            var limit = 10;
            service.GetWithPage(page, limit, order);

            service.RepositoryMock.Verify(r => r.GetPage(It.Is<PageRequest>(pr => pr.Order == order && pr.Ascending && pr.Page == page && pr.Limit == limit)), Times.Once());
        }

        [Test]
        public void GetWithPage_Order_Descending() {
            var service = ReadServiceMock.Create();
            var order = "Email";
            var page = 1;
            var limit = 10;
            service.GetWithPage(page, limit, $"-{order}");

            service.RepositoryMock.Verify(r => r.GetPage(It.Is<PageRequest>(pr => pr.Order == order && !pr.Ascending && pr.Page == page && pr.Limit == limit)), Times.Once());
        }

        [Test]
        public void Get_Calls_FindById_Method() {
            var service = ReadServiceMock.Create();
            var id = Guid.NewGuid();
            service.Get(id);

            service.RepositoryMock.Verify(r => r.FindById(id), Times.Once());
        }

        [Test]
        public void GetEntity_Throws_EntityNotFound_Exception() {
            var service = ReadServiceMock.Create();
            var id = Guid.NewGuid();
            service.RepositoryMock.Setup(r => r.FindById(id)).Returns((MockEntity)null);

            var exception = Assert.Throws<DenialException>(() => service.GetEntity(id));

            Assert.AreEqual(exception.Message, "EntityNotFound");
            Assert.AreEqual(exception.StatusCode, HttpStatusCode.NotFound);
        }
    }
}
