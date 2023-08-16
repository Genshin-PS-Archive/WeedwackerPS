using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Weedwacker.Shared.Enums
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum SceneDebugMode : byte
    {
        NONE,
        DEBUG,
        WARN,
        ERROR,
        ALL
    }
}
