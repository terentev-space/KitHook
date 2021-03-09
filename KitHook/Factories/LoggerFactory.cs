using System;
using KitHook.Services.ConfigService;
using KitHook.Services.ConfigService.Entities;
using Serilog;
using Serilog.Core;
using Serilog.Events;
using Serilog.Templates;

namespace KitHook.Factories
{
    public static class LoggerFactory
    {
        public static Logger Make(ConfigService config)
        {
            LoggerConfiguration loggerConfiguration = new LoggerConfiguration().MinimumLevel.Verbose();

            if (config is {LogConfig: { } logConfig})
            {
                if (logConfig.Console.Enabled)
                    loggerConfiguration.WriteTo.Console(
                        restrictedToMinimumLevel: (LogEventLevel) logConfig.Console.Level,
                        outputTemplate: logConfig.Console.Format
                    );

                if (logConfig.File.Enabled && logConfig.File.List is { } list)
                    foreach ((LogConfig.LogConfigLevel key, string value) in list)
                        loggerConfiguration.WriteTo.File(
                            path: String.Format(value, config.StartedAt, config.SessionId),
                            restrictedToMinimumLevel: (LogEventLevel) key,
                            rollingInterval: (RollingInterval) logConfig.File.Interval,
                            outputTemplate: logConfig.File.Format
                        );

                if (logConfig.Json.Enabled)
                    loggerConfiguration.WriteTo.File(
                        path: String.Format(logConfig.Json.Path, config.StartedAt, config.SessionId),
                        restrictedToMinimumLevel: (LogEventLevel) logConfig.Json.Level,
                        rollingInterval: (RollingInterval) logConfig.Json.Interval,
                        formatter: new ExpressionTemplate(logConfig.Json.Format)
                    );

                if (logConfig.Seq.Enabled)
                    loggerConfiguration.WriteTo.Seq(
                        serverUrl: logConfig.Seq.Uri,
                        restrictedToMinimumLevel: (LogEventLevel) logConfig.Json.Level
                    );
            }

            return loggerConfiguration.CreateLogger();
        }
    }
}