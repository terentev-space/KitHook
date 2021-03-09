using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace KitHook.Services.ConfigService.Entities
{
    public class LogSeqConfig
    {
        [JsonProperty(PropertyName = "enabled", Order = 1)]
        public bool Enabled { get; set; }
        
        [JsonProperty(PropertyName = "level", Order = 2)]
        [JsonConverter(typeof(StringEnumConverter))]
        public LogConfig.LogConfigLevel Level { get; set; }

        [JsonProperty(PropertyName = "uri", Order = 3)]
        public string Uri { get; set; }

        [JsonProperty(PropertyName = "key", Order = 4)]
        public string? Key { get; set; }
    }
}