using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace KitHook.Services.ConfigService.Entities
{
    public class LogConsoleConfig
    {
        [JsonProperty(PropertyName = "enabled", Order = 1)]
        public bool Enabled { get; set; }
        
        [JsonProperty(PropertyName = "level", Order = 2)]
        [JsonConverter(typeof(StringEnumConverter))]
        public LogConfig.LogConfigLevel Level { get; set; }

        [JsonProperty(PropertyName = "format", Order = 3)]
        public string Format { get; set; }
    }
}