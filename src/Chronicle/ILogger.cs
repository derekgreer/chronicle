using System;

namespace Chronicle
{
    public interface ILogger
    {
        void Write(LogEntry logEntry);
        bool IsEnabled(LogSeverityLevel severity);
        IDisposable BeginScope<TState>(TState state);
    }
}