using KitHook.Mediators.QueueSenderMediators.Interfaces;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace KitHook.Mediators.QueueSenderMediators.Entities
{
    public class QueueHttpMessageContent : IQueueHttpMessageContent
    {
        [JsonProperty(PropertyName = "type")]
        [JsonConverter(typeof(StringEnumConverter))]
        public IQueueHttpMessageContent.ContentType Type { get; set; }

        public QueueHttpMessageContent()
        {
        }
        
        public QueueHttpMessageContent(IQueueHttpMessageContent.ContentType type) => this.Type = type;
    }
}