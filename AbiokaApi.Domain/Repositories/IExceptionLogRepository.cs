using AbiokaApi.Infrastructure.Common.Domain;
using AbiokaApi.Infrastructure.Common.Exceptions;

namespace AbiokaApi.Domain.Repositories
{
    public interface IExceptionLogRepository : IReadOnlyRepository<ExceptionLog>
    {
        /// <summary>
        /// Adds the specified exception log.
        /// </summary>
        /// <param name="exceptionLog">The exception log.</param>
        void Add(ExceptionLog exceptionLog);
    }
}
