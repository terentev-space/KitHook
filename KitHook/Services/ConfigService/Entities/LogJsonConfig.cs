using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace KitHook.Services.ConfigService.Entities
{
    public class LogJsonConfig
    {
        [JsonProperty(PropertyName = "enabled", Order = 1)]
        public bool Enabled { get; set; }
        
        [JsonProperty(PropertyName = "level", Order = 2)]
        [JsonConverter(typeof(StringEnumConverter))]
        public LogConfig.LogConfigLevel Level { get; set; }

        [JsonProperty(PropertyName = "interval", Order = 3)]
        [JsonConverter(typeof(StringEnumConverter))]
        public LogConfig.LogConfigInterval Interval { get; set; }

        [JsonProperty(PropertyName = "format", Order = 4)]
        public string Format { get; set; }

        [JsonProperty(PropertyName = "path", Order = 5)]
        public string Path { get; set; }
    }
}