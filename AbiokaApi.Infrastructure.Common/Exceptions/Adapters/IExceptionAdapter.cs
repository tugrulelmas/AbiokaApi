using System.Collections.Generic;
using System.Net;

namespace AbiokaApi.Infrastructure.Common.Exceptions.Adapters
{
    public interface IExceptionAdapter
    {
        object Content { get; }

        IDictionary<string, string> ExtraHeaders { get; }

        HttpStatusCode HttpStatusCode { get; }
    }
}