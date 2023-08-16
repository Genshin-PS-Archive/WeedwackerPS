using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Weedwacker.Shared.Enums
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum AbilityDebugMode
    {
        NONE,
        INFO,
        WARN,
        ERROR,
        ALL
    }
}
