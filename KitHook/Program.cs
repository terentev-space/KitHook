using System;
using System.Threading;
using System.Threading.Tasks;
using KitHook.Extensions;
using KitHook.Services.ConfigService;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Serilog.Core;
using Serilog.Events;

namespace KitHook
{
    internal static class Program
    {
        private static ConfigService config;
        private static IServiceCollection services;
        private static IServiceProvider provider;

        private static App app;
        private static Logger? logger;
        private static CancellationTokenSource? cancellation;

        private static async Task Main(string[] args)
        {
            try
            {
                Program.config = new ConfigService();
                Startup.Configure(Program.config);

                Program.services = new ServiceCollection();
                Program.services.AddSingleton<ConfigService>(Program.config);

                Program.services.AddSingleton<App>();
                Startup.ConfigureServices(Program.services, Program.config);

                Program.provider = Program.services.BuildServiceProvider();

                Program.app = provider.GetRequiredService<App>();
                Program.logger = provider.GetService<Logger>();
                Program.cancellation = provider.GetService<CancellationTokenSource>();

                Console.CancelKeyPress += (_, eventArgs) =>
                {
                    eventArgs.Cancel = true;
                    Program.cancellation?.Cancel();
                };

                await Program.app.RunAsync();

                await TaskExtension.Delay(-1, Program.cancellation);
            }
            catch (TaskCanceledException ex)
            {
                Program.Out(LogEventLevel.Debug, ex.Message);
            }
            catch (Exception ex)
            {
                Program.Out(LogEventLevel.Fatal, ex);
            }
            finally
            {
                Log.CloseAndFlush();
            }

            Program.Out(LogEventLevel.Information, "The End!");
        }

        private static void Out(LogEventLevel level, string messageTemplate, params object[] propertyValues)
        {
            if (Program.logger is { })
                Program.logger?.Write(level, messageTemplate, propertyValues);
            else
                Console.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss} {level}] {messageTemplate}", propertyValues);
        }

        private static void Out(LogEventLevel level, Exception exception)
        {
            if (Program.logger is { })
                Program.logger?.Write(level, exception, exception.Message);
            else
            {
                Console.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss} {level}] {exception.Message}");
                Console.WriteLine(exception);
            }
        }
    }
}