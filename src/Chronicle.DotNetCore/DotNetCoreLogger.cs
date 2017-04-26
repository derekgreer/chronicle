using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using ILogger = Chronicle.ILogger;

namespace Chronicle.DotNetCore
{
    public class DotNetCoreLogger : ILogger
    {
        readonly Microsoft.Extensions.Logging.ILogger _logger;
        readonly Dictionary<LogSeverityLevel, LogWriterProvider> _map;

        public DotNetCoreLogger(ILoggerFactory loggingFactory, string categoryName)
        {
            _logger = loggingFactory.CreateLogger(categoryName);

            _map =
                new Dictionary<LogSeverityLevel, LogWriterProvider>
                {
                    {LogSeverityLevel.Verbose, e => _logger.LogTrace(e.ToString())},
                    {LogSeverityLevel.Debug, e => _logger.LogDebug(e.ToString())},
                    {LogSeverityLevel.Information, e => _logger.LogInformation(e.ToString())},
                    {LogSeverityLevel.Warning, e => _logger.LogWarning(e.ToString())},
                    {LogSeverityLevel.Error, e => _logger.LogError(e.ToString())},
                    {LogSeverityLevel.Critical, e => _logger.LogCritical(e.ToString())}
                };
        }

        public void Write(LogEntry logEntry)
        {
            var traceEventType = logEntry.Severity;

            if (_map.ContainsKey(traceEventType))
                _map[traceEventType](logEntry);
        }

        public bool IsEnabled(LogSeverityLevel logSeverityLevel)
        {
            return _logger.IsEnabled((LogLevel) logSeverityLevel);
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            return _logger.BeginScope(state);
        }

        delegate void LogWriterProvider(LogEntry logEntry);
    }
}