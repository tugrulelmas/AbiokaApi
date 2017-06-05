using AbiokaApi.Domain.Repositories;
using AbiokaApi.Infrastructure.Common.ApplicationSettings;
using AbiokaApi.Infrastructure.Common.Dynamic;
using AbiokaApi.Infrastructure.Common.Exceptions;

namespace AbiokaApi.ApplicationService.Handlers
{
    public class ExceptionLogHandler : IDynamicHandler
    {
        private readonly IExceptionLogResolver exceptionLogResolver;
        private readonly IExceptionLogRepository exceptionLogRepository;
        private readonly bool isExceptionLogEnabled = false;

        public ExceptionLogHandler(IExceptionLogResolver exceptionLogResolver, IExceptionLogRepository exceptionLogRepository, IConnectionStringRepository connectionStringRepository) {
            this.exceptionLogResolver = exceptionLogResolver;
            this.exceptionLogRepository = exceptionLogRepository;

            isExceptionLogEnabled = connectionStringRepository.ReadAppSetting("IsExceptionLogEnabled") == "true";
        }

        public short Order => 99;

        public void AfterSend(IResponseContext responseContext) {
        }

        public void BeforeSend(IRequestContext requestContext) {
        }

        public void OnException(IExceptionContext exceptionContext) {
            if (!isExceptionLogEnabled)
                return;

            try {
                var exceptionLog = exceptionLogResolver.Resolve(exceptionContext);
                exceptionLogRepository.Add(exceptionLog);
            } catch {
                // TODO: log to file system.
            }
        }
    }
}
