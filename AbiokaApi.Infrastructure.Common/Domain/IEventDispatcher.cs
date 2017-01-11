using System.Threading.Tasks;

namespace AbiokaApi.Infrastructure.Common.Domain
{
    public interface IEventDispatcher
    {
        /// <summary>
        /// Dispatches the specified events.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="events">The events.</param>
        void Dispatch<T>(params T[] events) where T : IEvent;

        /// <summary>
        /// Dispatches the asynchronous.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="events">The events.</param>
        /// <returns></returns>
        Task DispatchAsync<T>(params T[] events) where T : IEvent;
    }
}
