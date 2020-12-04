using System;

namespace Chronicle.Configuration
{
    public class LoggingConfigurationContext: ILoggingConfigurationContext, ILoggingConfiguration
    {
        public Func<Type, ILogger> LoggerProvider { get; set; }
       
        public ILoggingConfigurationContext SetLoggerProvider(Func<Type, ILogger> loggerProvider)
        {
            LoggerProvider = loggerProvider;
            return this;
        }
    }
}