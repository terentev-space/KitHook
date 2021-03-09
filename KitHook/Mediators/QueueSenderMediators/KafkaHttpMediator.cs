using System;
using System.Threading.Tasks;
using Confluent.Kafka;
using KitHook.Factories;
using KitHook.Mediators.QueueSenderMediators.Entities;
using KitHook.Services.QueueService.Entities;
using KitHook.Services.SenderService;
using KitHook.Services.SenderService.Entities;
using Newtonsoft.Json;
using Serilog.Core;

namespace KitHook.Mediators.QueueSenderMediators
{
    public class KafkaHttpMediator : KafkaMediator
    {
        private readonly HttpSenderService sender;
        private readonly Logger? logger;

        public KafkaHttpMediator(HttpSenderService sender, Logger? logger = null)
        {
            this.sender = sender;
            this.logger = logger;
        }

        public override Task<bool> CheckAsync(QueueMessage.QueueMessageType messageType) =>
            Task.FromResult(messageType == QueueMessage.QueueMessageType.Http);

        public override async Task MediateAsync(Guid hash, ConsumeResult<Ignore, string> data)
        {
            try
            {
                string json = this.GetMessage(data);
                this.logger?.Verbose("{name} [{hash}] message json: {json}", this.GetName(), hash, json);
                QueueHttpMessage message = this.DeserializeMessage(json);
                this.logger?.Verbose("{name} [{hash}] message object: {message}", this.GetName(), hash, JsonConvert.SerializeObject(message));
                HttpRequest request = HttpFactory.MakeRequest(message);
                this.logger?.Verbose("{name} [{hash}] request: {request}", this.GetName(), hash, JsonConvert.SerializeObject(request));
                await this.sender.SendAsync(request);
                this.logger?.Debug("{name} [{hash}] request status: {status}!", this.GetName(), hash, "success");
            }
            catch (Exception ex)
            {
                this.logger?.Warning(ex, "{name} [{hash}] request status: {status}. Message: {message}", this.GetName(), hash, "fail", ex.Message);
            }
        }

        public override string GetName() => base.GetName() + "->Http";

        private QueueHttpMessage DeserializeMessage(string message) => JsonConvert.DeserializeObject<QueueHttpMessage>(message);
    }
}