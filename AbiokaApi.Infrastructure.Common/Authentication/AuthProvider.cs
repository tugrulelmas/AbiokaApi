using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace AbiokaApi.Infrastructure.Common.Authentication
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum AuthProvider
    {
        Local,
        Google,
        Facebook
    }
}
