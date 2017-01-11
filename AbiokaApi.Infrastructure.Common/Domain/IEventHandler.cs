namespace AbiokaApi.Infrastructure.Common.Domain
{
    public interface IEventHandler<in T> where T : IEvent
    {
        /// <summary>
        /// Handles the specified event.
        /// </summary>
        /// <param name="eventInstance">The event instance.</param>
        void Handle(T eventInstance);
    }
}
