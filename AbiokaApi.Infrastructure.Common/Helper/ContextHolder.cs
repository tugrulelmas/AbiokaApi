using System.Runtime.Remoting.Messaging;
using System.Web;

namespace AbiokaApi.Infrastructure.Common.Helper
{
    public class ContextHolder : IContextHolder
    {
        public object GetData(string name) {
            return CallContext.LogicalGetData(name);
        }

        public void SetData(string name, object data) {
            CallContext.LogicalSetData(name, data);
        }
    }
}
