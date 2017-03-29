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

        [Test]
        public void RegisterAsAFactory() {
            DependencyContainer.Container
                .RegisterAsFactory<IServiceFactory>()
                .Register<IFooService, FooService>(LifeStyle.Transient);

            var serviceFactory = DependencyContainer.Container.Resolve<IServiceFactory>();
            var foo = "foo";
            var fooService = serviceFactory.CreateFooService(foo);

            Assert.AreEqual(foo, fooService.Foo());
        }
    }

    public interface IServiceFactory
    {
        IFooService CreateFooService(string foo);
    }

    public interface IFooService
    {
        string Foo();
    }

    public class FooService : IFooService
    {
        private readonly string foo;

        public FooService(string foo) {
            this.foo = foo;
        }

        public string Foo() {
            return foo;
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
