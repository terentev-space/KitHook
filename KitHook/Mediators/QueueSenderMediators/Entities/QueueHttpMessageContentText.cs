using KitHook.Mediators.QueueSenderMediators.Interfaces;
using Newtonsoft.Json;

namespace KitHook.Mediators.QueueSenderMediators.Entities
{
    public class QueueHttpMessageContentText : QueueHttpMessageContent
    {
        public const IQueueHttpMessageContent.ContentType CURRENT_CONTENT_TYPE = IQueueHttpMessageContent.ContentType.Text;
        
        [JsonProperty(PropertyName = "data")]
        public string Data { get; set; }

        public QueueHttpMessageContentText() : base(CURRENT_CONTENT_TYPE)
        {
        }
    }
}