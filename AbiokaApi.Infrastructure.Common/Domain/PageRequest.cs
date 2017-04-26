namespace AbiokaApi.Infrastructure.Common.Domain
{
    public class PageRequest
    {
        public int Page { get; set; }

        public int Limit { get; set; }

        private string order;

        public string Order {
            get {
                return order;
            }
            set {
                if (value.StartsWith("-")) {
                    order = value.Substring(1);
                    Ascending = false;
                } else {
                    order = value;
                    Ascending = true;
                }
            }
        }

        public bool Ascending { get; private set; }
    }
}
