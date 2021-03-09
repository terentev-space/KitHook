using System.Net.Http;

namespace KitHook.Services.SenderService.Entities
{
    public class HttpResponse
    {
        public bool IsSuccess { get; set; }
        public HttpResponseMessage? Message { get; set; } = null;
    }
}