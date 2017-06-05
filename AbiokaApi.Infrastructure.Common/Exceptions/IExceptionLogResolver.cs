using AbiokaApi.Infrastructure.Common.Dynamic;

namespace AbiokaApi.Infrastructure.Common.Exceptions
{
    public interface IExceptionLogResolver
    {
        ExceptionLog Resolve(IExceptionContext exceptionContext);
    }
}
