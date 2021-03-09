using System.Threading.Tasks;

namespace KitHook.Services.QueueService.Interfaces
{
    public interface IQueueCallback
    {
        public delegate Task QueueMessageCallback<in T>(T data);
    }
}