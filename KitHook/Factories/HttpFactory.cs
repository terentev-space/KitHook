using System.Net.Http;
using System.Text;
using KitHook.Mediators.QueueSenderMediators.Entities;
using KitHook.Mediators.QueueSenderMediators.Interfaces;
using KitHook.Services.SenderService.Entities;
using Newtonsoft.Json;

namespace KitHook.Factories
{
    public static class HttpFactory
    {
        private static readonly Encoding DefaultStringEncoding = Encoding.UTF8;

        public static HttpRequest MakeRequest(QueueHttpMessage message) => new HttpRequest()
        {
            Method = message.NetMethod,
            Uri = message.Uri,
            Content = MakeContent(message),
            Headers = message.Headers ?? null,
            Properties = message.Properties ?? null,
        };

        private static HttpContent? MakeContent(QueueHttpMessage message) =>
            HttpFactory.ConvertContentTo<QueueHttpMessageContent>(message)?.Type switch
            {
                IQueueHttpMessageContent.ContentType.Form => HttpFactory.MakeFormContent(message),
                IQueueHttpMessageContent.ContentType.Text => HttpFactory.MakeTextContent(message),
                IQueueHttpMessageContent.ContentType.Json => HttpFactory.MakeJsonContent(message),
                IQueueHttpMessageContent.ContentType.Byte => HttpFactory.MakeByteContent(message),
                IQueueHttpMessageContent.ContentType.Else => HttpFactory.MakeElseContent(message),
                _ => null,
            };

        private static HttpContent MakeFormContent(QueueHttpMessage message)
        {
            QueueHttpMessageContentForm content = HttpFactory.ConvertContentTo<QueueHttpMessageContentForm>(message);
            return new FormUrlEncodedContent(
                content.Data
            );
        }

        private static HttpContent MakeTextContent(QueueHttpMessage message)
        {
            QueueHttpMessageContentText content = HttpFactory.ConvertContentTo<QueueHttpMessageContentText>(message);
            return new StringContent(
                JsonConvert.SerializeObject(content.Data),
                DefaultStringEncoding
            );
        }

        private static HttpContent MakeJsonContent(QueueHttpMessage message)
        {
            QueueHttpMessageContentJson content = HttpFactory.ConvertContentTo<QueueHttpMessageContentJson>(message);
            return new StringContent(
                JsonConvert.SerializeObject(content.Data),
                DefaultStringEncoding,
                "application/json"
            );
        }

        private static HttpContent MakeByteContent(QueueHttpMessage message)
        {
            QueueHttpMessageContentByte content = HttpFactory.ConvertContentTo<QueueHttpMessageContentByte>(message);
            return new ByteArrayContent(
                content.Data
            );
        }

        private static HttpContent MakeElseContent(QueueHttpMessage message)
        {
            QueueHttpMessageContentElse content = HttpFactory.ConvertContentTo<QueueHttpMessageContentElse>(message);
            return new StringContent(
                content.Data,
                DefaultStringEncoding,
                content.Format
            );
        }

        private static T ConvertContentTo<T>(QueueHttpMessage message) where T : class, IQueueHttpMessageContent =>
            message.Content?.ToObject<T>();
    }
}