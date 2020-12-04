using System;

namespace Chronicle.Configuration
{
    public interface ILoggingConfigurationContext
    {
        ILoggingConfigurationContext SetLoggerProvider(Func<Type, ILogger> loggerProvider);
    }
}