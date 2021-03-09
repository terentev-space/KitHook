using KitHook.Mediators.QueueSenderMediators.Interfaces;
using Newtonsoft.Json;

namespace KitHook.Mediators.QueueSenderMediators.Entities
{
    public class QueueHttpMessageContentJson : QueueHttpMessageContent
    {
        public const IQueueHttpMessageContent.ContentType CURRENT_CONTENT_TYPE = IQueueHttpMessageContent.ContentType.Json;
        
        [JsonProperty(PropertyName = "data")]
        public object Data { get; set; }

        public QueueHttpMessageContentJson() : base(CURRENT_CONTENT_TYPE)
        {
        }
    }
}