using System;

namespace Chronicle.Configuration
{
    interface ILoggingConfiguration
    {
        Func<Type, ILogger> LoggerProvider { get; }
    }
}