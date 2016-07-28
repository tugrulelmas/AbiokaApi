namespace AbiokaApi.Infrastructure.Common.Dynamic
{
    public interface IDynamicHandler
    {
        /// <summary>
        /// Befores the send.
        /// </summary>
        /// <param name="requestContext">The request context.</param>
        void BeforeSend(IRequestContext requestContext);

        /// <summary>
        /// Afters the send.
        /// </summary>
        /// <param name="responseContext">The response context.</param>
        void AfterSend(IResponseContext responseContext);

        /// <summary>
        /// Called when [exception].
        /// </summary>
        /// <param name="exceptionContext">The exception context.</param>
        void OnException(IExceptionContext exceptionContext);
    }
}
