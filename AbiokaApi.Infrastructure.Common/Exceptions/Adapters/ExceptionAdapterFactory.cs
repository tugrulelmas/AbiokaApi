using System;

namespace AbiokaApi.Infrastructure.Common.Exceptions.Adapters
{
    public class ExceptionAdapterFactory : IExceptionAdapterFactory
    {
        public IExceptionAdapter GetAdapter(Exception exception) {
            if (exception is ApiException) {
                return new ApiExceptionAdapter((ApiException)exception);
            }
            else if (exception is AggregateException) {
                return new AggregateExceptionAdapter((AggregateException)exception);
            }
            else if (exception is ArgumentNullException) {
                return new ArgumentNullExceptionAdapter((ArgumentNullException)exception);
            }
            else {
                return new ExceptionAdapter(exception);
            }
        }
    }
}