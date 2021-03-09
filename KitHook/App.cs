using System;
using System.Threading.Tasks;
using Confluent.Kafka;
using KitHook.Mediators.QueueSenderMediators;
using KitHook.Services.QueueService;
using KitHook.Services.QueueService.Interfaces;
using Newtonsoft.Json;
using RabbitMQ.Client.Events;
using Serilog.Core;

namespace KitHook
{
    public class App
    {
        private readonly IQueueService queueService;
        private readonly KafkaMediator kafkaMediator;
        private readonly RabbitMediator rabbitMediator;
        private readonly Logger? logger;

        public App(
            IQueueService queueService,
            KafkaMediator kafkaMediator,
            RabbitMediator rabbitMediator,
            Logger? logger = null
        )
        {
            this.queueService = queueService;
            this.kafkaMediator = kafkaMediator;
            this.rabbitMediator = rabbitMediator;
            this.logger = logger;
        }

        public async Task RunAsync()
        {
            this.logger?.Information("Application status: {status}", "configured");

            this.logger?.Debug("Queue service: {name}", this.queueService.GetName());
            this.logger?.Debug("Queue connection: {connection}", this.queueService.GetConnection());

            if (this.queueService is KafkaQueueService kafkaQueueService)
                kafkaQueueService.Callback += KafkaQueueServiceOnCallback;

            if (this.queueService is RabbitQueueService rabbitQueueService)
                rabbitQueueService.Callback += RabbitQueueServiceOnCallback;
            
            this.logger?.Information("Application status: {status}", "started");
            
            await this.queueService.RunAsync();
            
            this.logger?.Information("Application status: {status}", "finished");
        }

        private async Task KafkaQueueServiceOnCallback(ConsumeResult<Ignore, string> data)
        {
            Guid hash = Guid.NewGuid();
            
            this.logger?.Verbose("{name} [{hash}] callback: {json}", this.kafkaMediator.GetName(), hash, JsonConvert.SerializeObject(data));

            await this.kafkaMediator.MediateAsync(hash, data);
        }

        private async Task RabbitQueueServiceOnCallback(BasicDeliverEventArgs data)
        {
            Guid hash = Guid.NewGuid();

            this.logger?.Verbose("{name} [{hash}] callback: {json}", this.rabbitMediator.GetName(), hash, JsonConvert.SerializeObject(data));

            await this.rabbitMediator.MediateAsync(hash, data);
        }
    }
}