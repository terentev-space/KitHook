using System;
using System.Threading;
using System.Threading.Tasks;
using Confluent.Kafka;
using KitHook.Services.ConfigService.Entities;
using KitHook.Services.QueueService.Interfaces;
using Serilog.Core;

namespace KitHook.Services.QueueService
{
    public class KafkaQueueService : IQueueService
    {
        private readonly ConfigService.ConfigService configService;
        private readonly ConsumerConfig consumerConfig;
        private readonly Logger? logger;
        private readonly CancellationTokenSource? cancellationTokenSource;

        public string GetName() => QueueConfig.QueueConfigType.Kafka.ToString();
        public string GetConnection() => this.configService.QueueConfig.Kafka.GetConnection();

        public KafkaQueueService(ConfigService.ConfigService configService, ConsumerConfig consumerConfig, Logger? logger = null, CancellationTokenSource? cancellationTokenSource = null)
        {
            this.configService = configService;
            this.consumerConfig = consumerConfig;
            this.logger = logger;
            this.cancellationTokenSource = cancellationTokenSource;
        }

        public event IQueueCallback.QueueMessageCallback<ConsumeResult<Ignore, string>> Callback;

        public async Task RunAsync()
        {
            using IConsumer<Ignore, string> consumer = new ConsumerBuilder<Ignore, string>(this.consumerConfig).Build();
            
            try
            {
                consumer.Subscribe(this.configService.QueueConfig.Kafka.Topic);
                
                this.logger?.Debug("Subscribed to {topic}", this.configService.QueueConfig.Kafka.Topic);

                while (this.cancellationTokenSource?.IsCancellationRequested == false)
                {
                    try
                    {
                        await this.Callback?.Invoke(consumer.Consume(this.cancellationTokenSource.Token))!;
                    }
                    catch (ConsumeException ex)
                    {
                        this.logger?.Warning(ex, ex.Message);
                    }
                }
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
                consumer.Close();
            }
        }
    }
}