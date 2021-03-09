using System.Collections.Generic;
using System.Runtime.Serialization;
using KitHook.Services.ConfigService.Interfaces;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace KitHook.Services.ConfigService.Entities
{
    public class QueueConfig : IConfig
    {
        [JsonProperty(PropertyName = "type", Order = 1)]
        [JsonConverter(typeof(StringEnumConverter))]
        public QueueConfigType Type { get; set; }

        [JsonProperty(PropertyName = "kafka", Order = 2)]
        public QueueKafkaConfig Kafka { get; set; } = new QueueKafkaConfig();

        [JsonProperty(PropertyName = "rabbitmq", Order = 3)]
        public QueueRabbitConfig Rabbit { get; set; } = new QueueRabbitConfig();

        public string GetPath() => "config/queue.json";

        public void FillDefault()
        {
            this.Type = QueueConfigType.Kafka;
            this.Kafka = new QueueKafkaConfig()
            {
                Servers = new List<string>()
                {
                    "localhost:9092",
                },
                
                Group = "default",
                Topic = "kithook",
            };
            this.Rabbit = new QueueRabbitConfig()
            {
                Host = "localhost",
                Port = 5672,
                Username = "guest",
                Password = "guest",
                VirtualHost = "",
                
                Uri = null,//"amqp://guest:guest@localhost:5672/",
                
                Queue = "kithook",
            };
        }

        public bool IsValid()
        {
            if (this.Type != QueueConfigType.Kafka && this.Type != QueueConfigType.Rabbit)
                return false;
            
            return true;
        }
        
        public enum QueueConfigType
        {
            [EnumMember(Value = "kafka")]
            Kafka,
            [EnumMember(Value = "rabbitmq")]
            Rabbit,
        }
    }
}