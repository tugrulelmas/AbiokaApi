using System;

namespace AbiokaApi.Infrastructure.Common.Dynamic
{
    public interface IExceptionContext
    {
        Exception Exception { get; }
    }
}
