using AbiokaApi.Infrastructure.Common.IoC;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace AbiokaApi.Infrastructure.Common.Domain
{
    public class EventDispatcher : IEventDispatcher
    {
        public void Dispatch<T>(params T[] events) where T : IEvent {
            foreach (var eventItem in events) {
                if (eventItem == null)
                    throw new ArgumentNullException(nameof(eventItem), "Event can not be null.");

                var type = typeof(IEventHandler<>).MakeGenericType(eventItem.GetType());
                var handlers = DependencyContainer.Container.ResolveAll(type)?.Cast<IEventHandler>();
                if (handlers == null)
                    continue;

                var orderedHandlers = handlers.OrderBy(h => h.Order);
                foreach (var handler in orderedHandlers) {
                    var methodInfo = handler.GetType().GetMethod("Handle");
                    methodInfo.Invoke(handler, new object[] { eventItem });
                }
                //((dynamic)handler).Handle(eventItem);
            }
        }

        public async Task DispatchAsync<T>(params T[] events) where T : IEvent {
            await Task.Factory.StartNew(() => { Dispatch<T>(events); });
        }
    }
}
