using KitHook.Mediators.QueueSenderMediators.Interfaces;
using Newtonsoft.Json;

namespace KitHook.Mediators.QueueSenderMediators.Entities
{
    public class QueueHttpMessageContentElse : QueueHttpMessageContent
    {
        public const IQueueHttpMessageContent.ContentType CURRENT_CONTENT_TYPE = IQueueHttpMessageContent.ContentType.Else;
        
        [JsonProperty(PropertyName = "data")]
        public string Data { get; set; }
        
        [JsonProperty(PropertyName = "format")]
        public string Format { get; set; }

        public QueueHttpMessageContentElse() : base(CURRENT_CONTENT_TYPE)
        {
        }
    }
}