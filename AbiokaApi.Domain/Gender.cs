using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace AbiokaApi.Domain
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum Gender
    {
        Male,
        Female
    }
}
