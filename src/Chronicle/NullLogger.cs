using System;

namespace Chronicle
{
    class NullLogger : ILogger
    {
        public void Write(LogEntry logEntry)
        {

        }

        public bool IsEnabled(LogSeverityLevel logSeverityLevel)
        {
            return false;
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            return new NullDisposable();
        }
    }
}