using AbiokaApi.Infrastructure.Common.IoC;
using Castle.DynamicProxy;

namespace AbiokaApi.Infrastructure.Framework.IoC
{
    class DynamicInterceptor : IInterceptor
    {
        private readonly IDynamicInterceptor dynamicInterceptor;

        public DynamicInterceptor(IDynamicInterceptor dynamicInterceptor) {
            this.dynamicInterceptor = dynamicInterceptor;
        }

        public void Intercept(IInvocation invocation) {
            dynamicInterceptor.Intercept(new CastleInvocationContext(invocation));
        }
    }
}
