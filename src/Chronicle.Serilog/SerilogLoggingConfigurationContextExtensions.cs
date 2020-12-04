using System;
using Chronicle.Configuration;
using Serilog;

namespace Chronicle.Serilog
{
    public static class SerilogLoggingConfigurationContextExtensions
    {
        public static ILoggingConfigurationContext UseSerilog(this ILoggingConfigurationContext loggingConfigurationContext, Action<LoggerConfiguration> loggerConfiguration)
        {
            var config = new LoggerConfiguration();
            loggerConfiguration(config);
            Log.Logger = config.CreateLogger();
            loggingConfigurationContext.SetLoggerProvider(t => new SerilogLogger(t));
            return loggingConfigurationContext;
        }
    }
}