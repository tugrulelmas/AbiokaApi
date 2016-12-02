using System.Runtime.Remoting.Messaging;

namespace AbiokaApi.Infrastructure.Common.Helper
{
    public class ContextHolder : IContextHolder
    {
        public object GetData(string name) => CallContext.LogicalGetData(name);

        public void SetData(string name, object data) {
            CallContext.LogicalSetData(name, data);
        }
    }
}
