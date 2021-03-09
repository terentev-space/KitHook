using System.Collections.Generic;
using Newtonsoft.Json;

namespace KitHook.Services.ConfigService.Entities
{
    public class QueueKafkaConfig
    {
        [JsonProperty(PropertyName = "servers", Order = 1)]
        public List<string> Servers { get; set; }

        [JsonProperty(PropertyName = "group", Order = 2)]
        public string Group { get; set; }

        [JsonProperty(PropertyName = "topic", Order = 3)]
        public string Topic { get; set; }

        public string GetConnection() => string.Join($"/{this.Group},", this.Servers) + $"/{this.Group}";
    }
}