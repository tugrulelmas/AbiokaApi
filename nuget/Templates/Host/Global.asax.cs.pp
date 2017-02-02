using System;
using System.Web;
using System.Web.Http;
using System.Web.Http.Dispatcher;
using System.Web.Routing;

namespace $rootnamespace$
{
    public class Global : HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e) {
            Bootstrapper.Initialise();

            GlobalConfiguration.Configure(WebApiConfig.Register);
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            GlobalConfiguration.Configuration.Services.Replace(typeof(IHttpControllerActivator), new DIControllerActivator());
        }
    }
}