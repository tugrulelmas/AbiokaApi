using System.Linq;
using System.Web.Http;

namespace AbiokaApi.Host
{
    public class WebApiConfig
    {
        public static void Register(HttpConfiguration config) {
            // Web API configuration and services
            // Web API routes
            config.MapHttpAttributeRoutes();

            //var cors = new EnableCorsAttribute("*", "*", "*");
            //config.EnableCors(cors);

            config.Routes.MapHttpRoute(
               name: "DefaultApi",
               routeTemplate: "api/{controller}/{id}",
               defaults: new { id = RouteParameter.Optional }
           );
            config.MessageHandlers.Add(new CustomDelegatingHandler());

            var appXmlType = config.Formatters.XmlFormatter.SupportedMediaTypes.FirstOrDefault(t => t.MediaType == "application/xml");
            config.Formatters.XmlFormatter.SupportedMediaTypes.Remove(appXmlType);
        }
    }
}