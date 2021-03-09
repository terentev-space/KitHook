using System.Collections.Generic;
using System.Net.Http;

namespace KitHook.Services.SenderService.Entities
{
    public class HttpRequest
    {
        public HttpMethod Method { get; set; }
        
        public string Uri { get; set; }

        public HttpContent? Content { get; set; } = null;
        
        public IDictionary<string, string>? Properties { get; set; } = null;
        
        public IDictionary<string, string>? Headers { get; set; } = null;
    }
}