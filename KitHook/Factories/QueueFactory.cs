using System;
using Confluent.Kafka;
using KitHook.Services.ConfigService;
using KitHook.Services.ConfigService.Entities;
using KitHook.Services.QueueService;
using RabbitMQ.Client;

namespace KitHook.Factories
{
    public static class QueueFactory
    {
        public static ConsumerConfig MakeKafkaConsumerConfig(ConfigService config) => new ConsumerConfig
        {
            BootstrapServers = string.Join(",", config.QueueConfig.Kafka.Servers),
            GroupId = config.QueueConfig.Kafka.Group,
            AutoOffsetReset = AutoOffsetReset.Earliest,
        };

        public static ConnectionFactory MakeRabbitConnection(ConfigService config) => new ConnectionFactory()
        {
            Uri = config.QueueConfig.Rabbit.GetUri(),
            DispatchConsumersAsync = true,
        };

        public static Type MakeQueueServiceType(ConfigService config) => config.QueueConfig.Type switch
        {
            QueueConfig.QueueConfigType.Kafka => typeof(KafkaQueueService),
            QueueConfig.QueueConfigType.Rabbit => typeof(RabbitQueueService),
            _ => throw new ArgumentOutOfRangeException()
        };
    }
}