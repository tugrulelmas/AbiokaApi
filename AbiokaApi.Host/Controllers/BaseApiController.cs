using AbiokaApi.Host.Attributes;
using System.Web.Http;

namespace AbiokaApi.Host.Controllers
{
    [CustomActionFilter]
    public class BaseApiController : ApiController
    {
    }
}