using System;
using System.IO;
using KitHook.Services.ConfigService.Entities;
using KitHook.Services.ConfigService.Interfaces;
using Newtonsoft.Json;

namespace KitHook.Services.ConfigService
{
    public class ConfigService
    {
        public readonly DateTime StartedAt;
        public readonly string SessionId;
        
        public LogConfig LogConfig { get; private set; }
        public MainConfig MainConfig { get; private set; }
        public QueueConfig QueueConfig { get; private set; }

        public ConfigService()
        {
            this.StartedAt = DateTime.Now;
            this.SessionId = Guid.NewGuid().ToString();
        }
        
        public void Init()
        {
            this.LogConfig ??= new LogConfig();
            this.MainConfig ??= new MainConfig();
            this.QueueConfig ??= new QueueConfig();
            
            this.LogConfig.FillDefault();
            this.MainConfig.FillDefault();
            this.QueueConfig.FillDefault();

            if (this.LogConfig is { } logConfig)
            {
                if (this.IsExists(logConfig.GetPath()))
                    this.LogConfig = this.Load<LogConfig>(logConfig.GetPath());
                else
                    this.Save(logConfig.GetPath(), logConfig);
            }

            if (this.MainConfig is { } mainConfig)
            {
                if (this.IsExists(mainConfig.GetPath()))
                    this.MainConfig = this.Load<MainConfig>(mainConfig.GetPath());
                else
                    this.Save(mainConfig.GetPath(), mainConfig);
            }

            if (this.QueueConfig is { } queueConfig)
            {
                if (this.IsExists(queueConfig.GetPath()))
                    this.QueueConfig = this.Load<QueueConfig>(queueConfig.GetPath());
                else
                    this.Save(queueConfig.GetPath(), queueConfig);
            }
        }
        
        public bool IsExists(string path) => File.Exists(path);
        
        public T Load<T>(string path) where T : class, IConfig => JsonConvert.DeserializeObject<T>(File.ReadAllText(path));

        public void Save(string path, IConfig config)
        {
            FileInfo file = new FileInfo(path);
            if (file.Directory?.Exists == false)
                Directory.CreateDirectory(file.Directory.FullName);
            File.WriteAllText(path, JsonConvert.SerializeObject(config, Formatting.Indented));
        }
    }
}