﻿using System.Collections.Generic;
using System.Web.Http.Controllers;
using System.Web.Http.Routing;

namespace AbiokaApi.Infrastructure.Framework.RestHelper.Attributes
{
    public class CustomDirectRouteProvider : DefaultDirectRouteProvider
    {
        protected override IReadOnlyList<IDirectRouteFactory> GetActionRouteFactories(HttpActionDescriptor actionDescriptor) => actionDescriptor.GetCustomAttributes<IDirectRouteFactory>(true);
    }
}