using System.Net.Http;
using System.Threading;
using Confluent.Kafka;
using KitHook.Factories;
using KitHook.Mediators.QueueSenderMediators;
using KitHook.Services.ConfigService;
using KitHook.Services.QueueService.Interfaces;
using KitHook.Services.SenderService;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;
using Serilog.Core;

namespace KitHook
{
    public static class Startup
    {
        public static void Configure(ConfigService config)
        {
            config.Init();
        }
        
        public static void ConfigureServices(IServiceCollection services, ConfigService config) => services
            .AddSingleton<CancellationTokenSource>()
            .AddSingleton<Logger>(LoggerFactory.Make(config))

            #region HttpSenderService Components

            .AddSingleton<HttpClient>()

            #endregion

            #region KafkaQueueService Components

            .AddSingleton<ConsumerConfig>(QueueFactory.MakeKafkaConsumerConfig(config))

            #endregion

            #region RabbitQueueService Components

            .AddSingleton<ConnectionFactory>(QueueFactory.MakeRabbitConnection(config))
            
            #endregion

            #region Components

            .AddSingleton<HttpSenderService>()
            
            #endregion

            #region Mediators

            .AddSingleton<KafkaMediator, KafkaHttpMediator>()
            .AddSingleton<RabbitMediator, RabbitHttpMediator>()
            
            #endregion

            .AddSingleton(typeof(IQueueService), QueueFactory.MakeQueueServiceType(config))
        ;
    }
}