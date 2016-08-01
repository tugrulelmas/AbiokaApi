using AbiokaApi.UnitTest.IoC.Mock;
using NUnit.Framework;
using System.Threading.Tasks;

namespace AbiokaApi.UnitTest.IoC
{
    [TestFixture]
    public class ContextHolderTest
    {
        [Test]
        public void ContextHolder_SetAndGetData() {
            var contextHolder = ContextHolderMock.Create();
            var name = "contextHolderTest";
            var contextDummy = new ContextDummy
            {
                Id = 12,
                Name = "Test"
            };
            contextHolder.SetData(name, contextDummy);
            var contextFromHolder = (ContextDummy)contextHolder.GetData(name);

            Assert.AreEqual(contextDummy, contextFromHolder);
        }

        [Test]
        public void ContextHolder_SetAndGetData_MultiThread() {
            var contextHolder = ContextHolderMock.Create();
            var name = "contextHolderTest";
            var contextDummy = new ContextDummy
            {
                Id = 12,
                Name = "Test"
            };
            contextHolder.SetData(name, contextDummy);
            var contextFromHolder = Task.Factory.StartNew(() =>
            {
                return (ContextDummy)contextHolder.GetData(name);
            }).Result;

            Assert.AreEqual(contextDummy, contextFromHolder);
        }
    }

    class ContextDummy
    {
        public string Name { get; set; }

        public int Id { get; set; }
    }
}
