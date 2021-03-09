using System;
using System.Text;
using System.Threading.Tasks;
using KitHook.Mediators.QueueSenderMediators.Interfaces;
using KitHook.Services.QueueService.Entities;
using RabbitMQ.Client.Events;

namespace KitHook.Mediators.QueueSenderMediators
{
    public abstract class RabbitMediator : IQueueSenderMediator<BasicDeliverEventArgs>
    {
        public abstract Task<bool> CheckAsync(QueueMessage.QueueMessageType messageType);
        public abstract Task MediateAsync(Guid hash, BasicDeliverEventArgs data);

        public virtual string GetName() => "RabbitMq";
        
        protected string GetMessage(BasicDeliverEventArgs data) => Encoding.UTF8.GetString(data.Body.ToArray());
    }
}