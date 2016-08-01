namespace AbiokaApi.Infrastructure.Common.Helper
{
    public interface IContextHolder
    {
        object GetData(string name);

        void SetData(string name, object data);
    }
}
