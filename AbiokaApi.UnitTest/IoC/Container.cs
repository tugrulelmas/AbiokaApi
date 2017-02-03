using AbiokaApi.Infrastructure.Common.IoC;
using AbiokaApi.Infrastructure.Framework.IoC;
using NUnit.Framework;
using System;

namespace AbiokaApi.UnitTest.IoC
{
    [TestFixture]
    public class Container
    {
        [SetUp]
        protected void SetUp() {
            DependencyContainer.SetContainer(new CastleContainer());

        }

        [Test]
        public void RegisterAndResolve() {
            DependencyContainer.Container.Register<ServiceInterceptor>(LifeStyle.Transient)
                .RegisterServices<IDummyService, IDummyService>()
                .Register<IServiceInterceptor, Interceptor>();

            var dummy = DependencyContainer.Container.Resolve<IDummyService>();

            Assert.AreEqual(dummy.GetText(), new DummyService().GetText());
        }
    }

   public interface IDummyService
    {
        string GetText();
    }

    public class DummyService : IDummyService
    {
        public string GetText() {
            return "Dummy";
        }
    }

    class Interceptor : IServiceInterceptor
    {
        public int Order => 10;

        public void BeforeProceed(IInvocationContext context) {
            Console.WriteLine("Before");
        }
    }

}
