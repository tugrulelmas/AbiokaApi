using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Dispatcher;
using System.Web.Security;
using System.Web.SessionState;

namespace AbiokaApi.Host
{
    public class Global : HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e) {
            GlobalConfiguration.Configure(WebApiConfig.Register);

            Bootstrapper.Initialise();

            GlobalConfiguration.Configuration.Services.Replace(typeof(IHttpControllerActivator), new DIControllerActivator());
        }
    }
}