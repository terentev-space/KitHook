using System;
using KitHook.Services.ConfigService.Interfaces;
using KitHook.Services.QueueService.Entities;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace KitHook.Services.ConfigService.Entities
{
    public class MainConfig : IConfig
    {
        [JsonProperty(PropertyName = "name", Order = 1)]
        public string Name { get; set; }
        
        [JsonProperty(PropertyName = "hash", Order = 2)]
        public string Hash { get; set; }
        
        [JsonProperty(PropertyName = "default_sender_type", Order = 3)]
        [JsonConverter(typeof(StringEnumConverter))]
        public QueueMessage.QueueMessageType DefaultSenderType { get; set; }

        public string GetPath() => "config/main.json";

        public void FillDefault()
        {
            this.Name = "KitHook";
            this.Hash = Guid.NewGuid().ToString();
            this.DefaultSenderType = QueueMessage.QueueMessageType.Http;
        }

        public bool IsValid()
        {
            return true;
        }
    }
}