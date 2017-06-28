namespace AbiokaApi.Infrastructure.Common.Domain
{
    public interface IEventHandler<T> : IEventHandler where T : IEvent
    {
        /// <summary>
        /// Handles the specified event.
        /// </summary>
        /// <param name="eventInstance">The event instance.</param>
        void Handle(T eventInstance);
    }

    public interface IEventHandler
    {
        /// <summary>
        /// Gets the order.
        /// </summary>
        /// <value>
        /// The order.
        /// </value>
        int Order { get; }
    }
}
