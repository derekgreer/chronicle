using System;
using Chronicle.Context;

namespace Chronicle.Configuration
{
    public static class LoggingConfiguration
    {
        public static void Configure(Action<ILoggingConfigurationContext> loggingConfigurationContext)
        {
            var context = new LoggingConfigurationContext();
            loggingConfigurationContext(context);

            if (context.LoggerProvider != null)
                LogContext.LoggerProvider = context.LoggerProvider;
        }
    }
}