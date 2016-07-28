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
            DependencyContainer.Container.Register<IDummy, Dummy>();

            var dummy = DependencyContainer.Container.Resolve<IDummy>();

            Assert.AreEqual(dummy.GetText(), new Dummy().GetText());
        }
    }

    interface IDummy
    {
        string GetText();
    }

    class Dummy : IDummy
    {
        public string GetText() {
            return "Dummy";
        }
    }

    class Interceptor : IDynamicInterceptor
    {
        public void Intercept(IInvocationContext context) {
            Console.WriteLine("Before");
            context.Proceed();
            Console.WriteLine("After");
        }
    }
}
