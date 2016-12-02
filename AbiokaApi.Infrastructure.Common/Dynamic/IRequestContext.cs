using System.Net.Http;

namespace AbiokaApi.Infrastructure.Common.Dynamic
{
    public interface IRequestContext
    {
        HttpRequestMessage Request { get; }
    }
}
