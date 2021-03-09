using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using KitHook.Services.QueueService.Entities;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using NetHttpMethod = System.Net.Http.HttpMethod;

namespace KitHook.Mediators.QueueSenderMediators.Entities
{
    public class QueueHttpMessage : QueueMessage
    {
        [JsonProperty(PropertyName = "method")]
        [JsonConverter(typeof(StringEnumConverter))]
        public HttpMethod Method { get; set; }
        
        [JsonProperty(PropertyName = "uri")]
        public string Uri { get; set; }

        [JsonProperty(PropertyName = "content")]
        public JObject? Content { get; set; } = null;
        
        [JsonProperty(PropertyName = "properties")]
        public IDictionary<string, string>? Properties { get; set; } = null;
        
        [JsonProperty(PropertyName = "headers")]
        public IDictionary<string, string>? Headers { get; set; } = null;

        [JsonIgnore]
        public NetHttpMethod NetMethod => this.Method switch
        {
            HttpMethod.Get => NetHttpMethod.Get,
            HttpMethod.Put => NetHttpMethod.Put,
            HttpMethod.Post => NetHttpMethod.Post,
            HttpMethod.Delete => NetHttpMethod.Delete,
            HttpMethod.Head => NetHttpMethod.Head,
            HttpMethod.Options => NetHttpMethod.Options,
            HttpMethod.Patch => NetHttpMethod.Patch,
            HttpMethod.Trace => NetHttpMethod.Trace,
            _ => throw new ArgumentOutOfRangeException(),
        };
        
        public enum HttpMethod
        {
            [EnumMember(Value = "get")]
            Get,
            [EnumMember(Value = "put")]
            Put,
            [EnumMember(Value = "post")]
            Post,
            [EnumMember(Value = "delete")]
            Delete,
            [EnumMember(Value = "head")]
            Head,
            [EnumMember(Value = "options")]
            Options,
            [EnumMember(Value = "patch")]
            Patch,
            [EnumMember(Value = "trace")]
            Trace,
        }
    }
}