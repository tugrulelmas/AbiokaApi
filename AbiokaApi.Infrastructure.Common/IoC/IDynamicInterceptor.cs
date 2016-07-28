namespace AbiokaApi.Infrastructure.Common.IoC
{
    public interface IDynamicInterceptor
    {
        void Intercept(IInvocationContext context);
    }
}
