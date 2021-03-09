using KitHook.Mediators.QueueSenderMediators.Interfaces;
using Newtonsoft.Json;

namespace KitHook.Mediators.QueueSenderMediators.Entities
{
    public class QueueHttpMessageContentByte : QueueHttpMessageContent
    {
        public const IQueueHttpMessageContent.ContentType CURRENT_CONTENT_TYPE = IQueueHttpMessageContent.ContentType.Byte;
        
        [JsonProperty(PropertyName = "data")]
        public byte[] Data { get; set; }

        public QueueHttpMessageContentByte() : base(CURRENT_CONTENT_TYPE)
        {
        }
    }
}