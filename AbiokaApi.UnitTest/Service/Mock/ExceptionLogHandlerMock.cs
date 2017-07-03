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
        public readonly Mock<IConfigurationManager> ConfigurationManagerMock;

        public ExceptionLogHandlerMock(Mock<IExceptionLogResolver> exceptionLogResolver, Mock<IExceptionLogRepository> exceptionLogRepository, Mock<IConfigurationManager> configurationManager)
            : base(exceptionLogResolver.Object, exceptionLogRepository.Object, configurationManager.Object) {
            ExceptionLogResolverMock = exceptionLogResolver;
            ExceptionLogRepositoryMock = exceptionLogRepository;
            ConfigurationManagerMock = configurationManager;
        }

        public static ExceptionLogHandlerMock Create(Mock<IConfigurationManager> connectionStringRepository) => new ExceptionLogHandlerMock(new Mock<IExceptionLogResolver>(), new Mock<IExceptionLogRepository>(), connectionStringRepository);
    }
}
