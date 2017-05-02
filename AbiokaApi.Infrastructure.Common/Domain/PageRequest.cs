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
                if (string.IsNullOrEmpty(value) || char.IsLetterOrDigit(value, 0)) {
                    order = value;
                    Ascending = true;
                    return;
                }

                order = value.Substring(1);
                Ascending = false;
            }
        }

        public bool Ascending { get; private set; }
    }
}
