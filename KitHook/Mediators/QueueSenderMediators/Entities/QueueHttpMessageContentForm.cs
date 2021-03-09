using System.Collections.Generic;
using KitHook.Mediators.QueueSenderMediators.Interfaces;
using Newtonsoft.Json;

namespace KitHook.Mediators.QueueSenderMediators.Entities
{
    public class QueueHttpMessageContentForm : QueueHttpMessageContent
    {
        public const IQueueHttpMessageContent.ContentType CURRENT_CONTENT_TYPE = IQueueHttpMessageContent.ContentType.Form;
        
        [JsonProperty(PropertyName = "data")]
        public Dictionary<string, string> Data { get; set; }

        public QueueHttpMessageContentForm() : base(CURRENT_CONTENT_TYPE)
        {
        }
    }
}