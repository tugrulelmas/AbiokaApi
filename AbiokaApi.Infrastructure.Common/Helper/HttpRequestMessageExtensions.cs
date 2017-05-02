using System.Collections.Generic;
using System.Net.Http;
using System.Linq;

namespace AbiokaApi.Infrastructure.Common.Helper
{
    public static class HttpRequestMessageExtensions
    {
        private const string httpContext = "MS_HttpContext";
        private const string remoteEndpointMessage = "System.ServiceModel.Channels.RemoteEndpointMessageProperty";
        private const string owinContext = "MS_OwinContext";
        private const string cloudFlare = "CF-Connecting-IP";

        public static string GetClientIpString(this HttpRequestMessage request) {
            //"CF-Connecting-IP"
            if (request.Headers.TryGetValues(cloudFlare, out IEnumerable<string> clientIPs)) {
                if(clientIPs!= null && clientIPs.Count() > 0) {
                    var clientIp = clientIPs.First();
                    if (!string.IsNullOrWhiteSpace(clientIp)) {
                        return clientIp;
                    }
                }
            }

            //Web-hosting
            if (request.Properties.ContainsKey(httpContext)) {
                dynamic ctx = request.Properties[httpContext];
                if (ctx != null) {
                    return ctx.Request.UserHostAddress;
                }
            }
            //Self-hosting
            if (request.Properties.ContainsKey(remoteEndpointMessage)) {
                dynamic remoteEndpoint = request.Properties[remoteEndpointMessage];
                if (remoteEndpoint != null) {
                    return remoteEndpoint.Address;
                }
            }
            //Owin-hosting
            if (request.Properties.ContainsKey(owinContext)) {
                dynamic ctx = request.Properties[owinContext];
                if (ctx != null) {
                    return ctx.Request.RemoteIpAddress;
                }
            }
            if (System.Web.HttpContext.Current != null) {
                return System.Web.HttpContext.Current.Request.UserHostAddress;
            }
            // Always return all zeroes for any failure
            return "0.0.0.0";
        }
    }
}
