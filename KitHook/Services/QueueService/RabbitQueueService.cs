using System;
using System.Threading;
using System.Threading.Tasks;
using KitHook.Extensions;
using KitHook.Services.ConfigService.Entities;
using KitHook.Services.QueueService.Interfaces;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Serilog.Core;

namespace KitHook.Services.QueueService
{
    public class RabbitQueueService : IQueueService
    {
        private readonly ConfigService.ConfigService configService;
        private readonly ConnectionFactory connectionFactory;
        private readonly Logger? logger;
        private readonly CancellationTokenSource? cancellationTokenSource;

        public string GetName() => QueueConfig.QueueConfigType.Rabbit.ToString();
        public string GetConnection() => this.configService.QueueConfig.Rabbit.GetConnection();

        public RabbitQueueService(ConfigService.ConfigService configService, ConnectionFactory connectionFactory, Logger? logger = null, CancellationTokenSource? cancellationTokenSource = null)
        {
            this.configService = configService;
            this.connectionFactory = connectionFactory;
            this.logger = logger;
            this.cancellationTokenSource = cancellationTokenSource;
        }

        public event IQueueCallback.QueueMessageCallback<BasicDeliverEventArgs> Callback;

        public async Task RunAsync()
        {
            using IConnection connection = this.connectionFactory.CreateConnection();
            using IModel channel = connection.CreateModel();

            try
            {
                AsyncEventingBasicConsumer consumer = new AsyncEventingBasicConsumer(channel);

                consumer.Received += async (_, eventArgs) =>
                {
                    await this.Callback?.Invoke(eventArgs)!;
                    await Task.Yield();
                };

                channel.BasicConsume(
                    queue: this.configService.QueueConfig.Rabbit.Queue,
                    autoAck: true,
                    consumer: consumer
                );

                this.logger?.Debug("Subscribed to {queue}", this.configService.QueueConfig.Rabbit.Queue);

                await TaskExtension.Delay(-1, this.cancellationTokenSource);
            }
            catch (OperationCanceledException ex)
            {
                this.logger?.Debug(ex.Message);
            }
            catch (Exception ex)
            {
                this.logger?.Error(ex, ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }
    }
}