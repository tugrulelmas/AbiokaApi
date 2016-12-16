using AbiokaApi.Infrastructure.Common.Authentication;

namespace AbiokaApi.Infrastructure.Common.Helper
{
    public interface ICurrentContext
    {
        ICurrentContext Current { get; }

        ICustomPrincipal Principal { get; set; }

        ActionType ActionType { get; set; }
    }
}
