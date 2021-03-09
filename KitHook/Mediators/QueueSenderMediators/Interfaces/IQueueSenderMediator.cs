using System;
using System.Threading.Tasks;
using KitHook.Services.QueueService.Entities;

namespace KitHook.Mediators.QueueSenderMediators.Interfaces
{
    public interface IQueueSenderMediator<in T>
    {
        public Task<bool> CheckAsync(QueueMessage.QueueMessageType messageType);
        public Task MediateAsync(Guid hash, T data);
    }
}