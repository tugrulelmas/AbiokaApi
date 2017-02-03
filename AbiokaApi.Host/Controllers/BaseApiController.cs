using AbiokaApi.Infrastructure.Framework.RestHelper.Attributes;
using System.Web.Http;

namespace AbiokaApi.Host.Controllers
{
    [CustomActionFilter]
    [CustomExceptionFilter]
    public class BaseApiController : ApiController
    {
    }
}