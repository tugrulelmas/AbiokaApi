using AbiokaApi.Infrastructure.Common.Helper;

namespace AbiokaApi.UnitTest.IoC.Mock
{
    class ContextHolderMock : ContextHolder
    {
        public static ContextHolderMock Create() {
            return new ContextHolderMock();
        }
    }
}
