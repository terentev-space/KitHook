using System;
using Newtonsoft.Json;

namespace KitHook.Services.ConfigService.Entities
{
    public class QueueRabbitConfig
    {
        [JsonProperty(PropertyName = "host", Order = 1)]
        public string Host { get; set; }
        
        [JsonProperty(PropertyName = "port", Order = 2)]
        public int Port { get; set; }
        
        [JsonProperty(PropertyName = "vhost", Order = 3)]
        public string VirtualHost { get; set; }

        [JsonProperty(PropertyName = "username", Order = 4)]
        public string Username { get; set; }

        [JsonProperty(PropertyName = "password", Order = 5)]
        public string Password { get; set; }

        [JsonProperty(PropertyName = "uri", Order = 6)]
        public string? Uri { get; set; } = null;

        [JsonProperty(PropertyName = "queue", Order = 7)]
        public string Queue { get; set; }

        public Uri GetUri() => new Uri(this.Uri ?? $"amqp://{this.Username}:{this.Password}@{this.Host}:{this.Port}/{this.VirtualHost}");

        public string GetConnection() => this.GetUri().ToString();
    }
}