using System.Threading;
using System.Threading.Tasks;

namespace KitHook.Extensions
{
    public static class TaskExtension
    {
        public static async Task Delay(int millisecondsDelay, CancellationTokenSource? cancellationTokenSource = null)
        {
            if (cancellationTokenSource is { })
                await Task.Delay(millisecondsDelay, cancellationTokenSource.Token);
            else
                await Task.Delay(millisecondsDelay);
        }
    }
}