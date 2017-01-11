using System.Collections.Generic;

namespace AbiokaApi.Infrastructure.Common.Domain
{
    public interface IEntity
    {
        IEnumerable<IEvent> Events { get; }
    }
}
