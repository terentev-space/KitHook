using System.Threading.Tasks;

namespace KitHook.Services.QueueService.Interfaces
{
    public interface IQueueService
    {
        public string GetName();
        public string GetConnection();
        
        public Task RunAsync();
    }
}