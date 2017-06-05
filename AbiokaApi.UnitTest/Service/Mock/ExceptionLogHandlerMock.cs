using AbiokaApi.ApplicationService.Handlers;
using AbiokaApi.Domain.Repositories;
using AbiokaApi.Infrastructure.Common.ApplicationSettings;
using AbiokaApi.Infrastructure.Common.Exceptions;
using Moq;

namespace AbiokaApi.UnitTest.Service.Mock
{
    class ExceptionLogHandlerMock : ExceptionLogHandler
    {
        public readonly Mock<IExceptionLogResolver> ExceptionLogResolverMock;
        public readonly Mock<IExceptionLogRepository> ExceptionLogRepositoryMock;
        public readonly Mock<IConnectionStringRepository> ConnectionStringRepositoryMock;

        public ExceptionLogHandlerMock(Mock<IExceptionLogResolver> exceptionLogResolver, Mock<IExceptionLogRepository> exceptionLogRepository, Mock<IConnectionStringRepository> connectionStringRepository)
            : base(exceptionLogResolver.Object, exceptionLogRepository.Object, connectionStringRepository.Object) {
            ExceptionLogResolverMock = exceptionLogResolver;
            ExceptionLogRepositoryMock = exceptionLogRepository;
            ConnectionStringRepositoryMock = connectionStringRepository;
        }

        public static ExceptionLogHandlerMock Create(Mock<IConnectionStringRepository> connectionStringRepository) => new ExceptionLogHandlerMock(new Mock<IExceptionLogResolver>(), new Mock<IExceptionLogRepository>(), connectionStringRepository);
    }
}
