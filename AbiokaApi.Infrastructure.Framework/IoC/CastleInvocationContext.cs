using AbiokaApi.Infrastructure.Common.IoC;
using Castle.DynamicProxy;

namespace AbiokaApi.Infrastructure.Framework.IoC
{
    class CastleInvocationContext : IInvocationContext
    {
        private readonly IInvocation invocation;

        public CastleInvocationContext(IInvocation invocation) {
            this.invocation = invocation;
        }

        public void Proceed() {
            invocation.Proceed();
        }
    }
}
