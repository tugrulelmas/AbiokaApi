using $rootnamespace$.Attributes;
using System.Web.Http;

namespace $rootnamespace$.Controllers
{
    [CustomActionFilter]
    [CustomExceptionFilter]
    public class BaseApiController : ApiController
    {
    }
}