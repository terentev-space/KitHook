using System;
using System.Net.Http;
using System.Threading.Tasks;
using KitHook.Services.SenderService.Entities;

namespace KitHook.Services.SenderService
{
    public class HttpSenderService
    {
        private readonly HttpClient client;

        public HttpSenderService(HttpClient client)
        {
            this.client = client;
        }

        public async Task<HttpResponse> SendAsync(HttpRequest request)
        {
            HttpRequestMessage requestMessage = new HttpRequestMessage
            {
                Method = request.Method,
                RequestUri = new Uri(request.Uri),
            };

            if (request.Content is { })
                requestMessage.Content = request.Content;

            if (request.Properties is { } properties)
                foreach ((string key, string value) in properties)
                    requestMessage.Properties.Add(key, value);

            if (request.Headers is { } headers)
                foreach ((string key, string value) in headers)
                    requestMessage.Headers.Add(key, value);

            HttpResponseMessage responseMessage = await this.client.SendAsync(requestMessage);
            return new HttpResponse()
            {
                IsSuccess = responseMessage.IsSuccessStatusCode,
                Message = responseMessage,
            };
        }
    }
}