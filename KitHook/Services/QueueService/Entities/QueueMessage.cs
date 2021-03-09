using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace KitHook.Services.QueueService.Entities
{
    public class QueueMessage
    {
        [JsonProperty(PropertyName = "id")]
        public string? Id { get; set; }
        
        [JsonProperty(PropertyName = "type")]
        [JsonConverter(typeof(StringEnumConverter))]
        public QueueMessageType? Type { get; set; }
        
        public enum QueueMessageType
        {
            [EnumMember(Value = "http")] Http,
        }
    }
}