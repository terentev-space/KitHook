using System;
using System.Threading.Tasks;
using Confluent.Kafka;
using KitHook.Mediators.QueueSenderMediators.Interfaces;
using KitHook.Services.QueueService.Entities;

namespace KitHook.Mediators.QueueSenderMediators
{
    public abstract class KafkaMediator : IQueueSenderMediator<ConsumeResult<Ignore, string>>
    {
        public abstract Task<bool> CheckAsync(QueueMessage.QueueMessageType messageType);
        public abstract Task MediateAsync(Guid hash, ConsumeResult<Ignore, string> data);

        public virtual string GetName() => "Kafka";

        protected string GetMessage(ConsumeResult<Ignore, string> data) => data.Message.Value;
    }
}