using System.Collections.Generic;
using System.Runtime.Serialization;
using KitHook.Services.ConfigService.Interfaces;
using Newtonsoft.Json;
using Serilog;
using Serilog.Events;

namespace KitHook.Services.ConfigService.Entities
{
    public class LogConfig : IConfig
    {
        [JsonProperty(PropertyName = "console", Order = 1)]
        public LogConsoleConfig Console { get; set; } = new LogConsoleConfig();
        
        [JsonProperty(PropertyName = "file", Order = 2)]
        public LogFileConfig File { get; set; } = new LogFileConfig();

        [JsonProperty(PropertyName = "json", Order = 3)]
        public LogJsonConfig Json { get; set; } = new LogJsonConfig();

        [JsonProperty(PropertyName = "seq", Order = 4)]
        public LogSeqConfig Seq { get; set; } = new LogSeqConfig();

        public string GetPath() => "config/log.json";

        public void FillDefault()
        {
            this.Console.Enabled = true;
            this.Console.Level = LogConfigLevel.Verbose;
            this.Console.Format = "[{Timestamp:yyyy-MM-dd HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}";
            
            this.File.Enabled = true;
            this.File.Level = LogConfigLevel.Verbose;
            this.File.Interval = LogConfigInterval.Day;
            this.File.Format = "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}";
            this.File.List = new Dictionary<LogConfigLevel, string>()
            {
                {LogConfigLevel.Verbose, "logs/{0:yyyy-MM-dd_HH-mm-ss}/{1}/verbose.txt"},
                {LogConfigLevel.Debug, "logs/{0:yyyy-MM-dd_HH-mm-ss}/{1}/debug.txt"},
                {LogConfigLevel.Information, "logs/{0:yyyy-MM-dd_HH-mm-ss}/{1}/information.txt"},
                {LogConfigLevel.Warning, "logs/{0:yyyy-MM-dd_HH-mm-ss}/{1}/warning.txt"},
                {LogConfigLevel.Error, "logs/{0:yyyy-MM-dd_HH-mm-ss}/{1}/error.txt"},
            };
            
            this.Json.Enabled = true;
            this.Json.Level = LogConfigLevel.Verbose;
            this.Json.Interval = LogConfigInterval.Day;
            this.Json.Format = "{ {timestamp: @t, message: @mt, level: @l, exception: @x, properties: @p} }\n";
            this.Json.Path = "logs/{0:yyyy-MM-dd_HH-mm-ss}/{1}/verbose.json";
            
            this.Seq.Enabled = false;
            this.Seq.Level = LogConfigLevel.Verbose;
            this.Seq.Uri = "http://localhost:5341";
            this.Seq.Key = null;
        }

        public bool IsValid()
        {
            return true;
        }

        public enum LogConfigLevel
        {
            [EnumMember(Value = "verbose")] Verbose = LogEventLevel.Verbose,
            [EnumMember(Value = "debug")] Debug = LogEventLevel.Debug,
            [EnumMember(Value = "information")] Information = LogEventLevel.Information,
            [EnumMember(Value = "warning")] Warning = LogEventLevel.Warning,
            [EnumMember(Value = "error")] Error = LogEventLevel.Error,
            [EnumMember(Value = "fatal")] Fatal = LogEventLevel.Fatal,
        }

        public enum LogConfigInterval
        {
            [EnumMember(Value = "minute")] Minute = RollingInterval.Minute,
            [EnumMember(Value = "hour")] Hour = RollingInterval.Hour,
            [EnumMember(Value = "day")] Day = RollingInterval.Day,
            [EnumMember(Value = "month")] Month = RollingInterval.Month,
            [EnumMember(Value = "year")] Year = RollingInterval.Year,
            [EnumMember(Value = "infinite")] Infinite = RollingInterval.Infinite,
        }
    }
}