using System;

namespace Chronicle.Context
{
    static class LogContext
    {
        public static Func<Type, ILogger> LoggerProvider { get; set; } = category => new NullLogger();
    }
}