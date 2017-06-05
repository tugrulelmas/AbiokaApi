namespace AbiokaApi.Infrastructure.Common.Exceptions
{
    public class ExceptionContent
    {
        public string ErrorCode { get; set; }

        public object[] Parameters { get; set; }
    }
}
