namespace AbiokaApi.Infrastructure.Common.Domain
{
    public class PageRequest
    {
        public int Page { get; set; }

        public int Limit { get; set; }

        public string Order { get; set; }

        public bool Ascending { get; set; }
    }
}
