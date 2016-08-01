using System.Runtime.Remoting.Messaging;
using System.Web;

namespace AbiokaApi.Infrastructure.Common.Helper
{
    public class ContextHolder : IContextHolder
    {
        public object GetData(string name) {
            if (HttpContext.Current != null)
            {
                return HttpContext.Current.Items[name];
            }
            else
            {
                return CallContext.LogicalGetData(name);
            }
        }

        public void SetData(string name, object data) {
            if (HttpContext.Current != null)
            {
                HttpContext.Current.Items[name] = data;
            }
            else
            {
                CallContext.LogicalSetData(name, data);
            }
        }
    }
}
