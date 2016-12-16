using AbiokaApi.Infrastructure.Common.Authentication;
using System;

namespace AbiokaApi.Infrastructure.Common.Helper
{
    public class CurrentContext : ICurrentContext
    {
        private static readonly string contextName = "_AbiokaContext_";
        private readonly IContextHolder contextHolder;

        public CurrentContext(IContextHolder contextHolder) {
            this.contextHolder = contextHolder;
            contextHolder.SetData(contextName, this);
        }

        public ActionType ActionType {
            get {
                throw new NotImplementedException();
            }
        }

        public ICurrentContext Current {
            get {
                object obj = contextHolder.GetData(contextName);
                if (obj == null) {
                    throw new ApplicationException("Abioka context is empty");
                }
                return (CurrentContext)obj;
            }
        }

        public ICustomPrincipal Principal { get; set; }

        ActionType ICurrentContext.ActionType { get; set; }
    }
}