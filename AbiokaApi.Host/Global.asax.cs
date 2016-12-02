using System;
using System.Web;
using System.Web.Http;
using System.Web.Http.Dispatcher;

namespace AbiokaApi.Host
{
    public class Global : HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e) {
            Bootstrapper.Initialise();

            GlobalConfiguration.Configure(WebApiConfig.Register);

            GlobalConfiguration.Configuration.Services.Replace(typeof(IHttpControllerActivator), new DIControllerActivator());
        }
    }
}