using AbiokaApi.Infrastructure.Common.ApplicationSettings;
using AbiokaApi.Infrastructure.Common.Dynamic;
using AbiokaApi.Infrastructure.Common.Exceptions;
using AbiokaApi.UnitTest.Service.Mock;
using Moq;
using NUnit.Framework;

namespace AbiokaApi.UnitTest.Service
{
    [TestFixture]
    public class ExceptionLogHandlerTest
    {
        [Test]
        public void Do_Log_When_IsExceptionLogEnabled() {
            var connectionStringRepository = new Mock<IConnectionStringRepository>();
            connectionStringRepository.Setup(c => c.ReadAppSetting("IsExceptionLogEnabled")).Returns("true");
            var exceptionLogHandlerMock = ExceptionLogHandlerMock.Create(connectionStringRepository);

            IExceptionContext exceptionContext = new ExceptionContext(null);
            var exceptionLog = new ExceptionLog();
            exceptionLogHandlerMock.ExceptionLogResolverMock.Setup(e => e.Resolve(exceptionContext)).Returns(exceptionLog);

            exceptionLogHandlerMock.OnException(exceptionContext);

            exceptionLogHandlerMock.ExceptionLogResolverMock.Verify(e => e.Resolve(exceptionContext), Times.Once());
            exceptionLogHandlerMock.ExceptionLogRepositoryMock.Verify(e => e.Add(exceptionLog), Times.Once());
        }

        [Test]
        public void Do_Not_Log_When_IsExceptionLogDisabled() {
            var connectionStringRepository = new Mock<IConnectionStringRepository>();
            connectionStringRepository.Setup(c => c.ReadAppSetting("IsExceptionLogEnabled")).Returns("false");
            var exceptionLogHandlerMock = ExceptionLogHandlerMock.Create(connectionStringRepository);

            IExceptionContext exceptionContext = new ExceptionContext(null);
            exceptionLogHandlerMock.OnException(exceptionContext);

            exceptionLogHandlerMock.ExceptionLogResolverMock.Verify(e => e.Resolve(exceptionContext), Times.Never());
        }
    }
}
