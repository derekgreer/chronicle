using System;
using System.Collections.Generic;
using System.Linq;
using Chronicle.Internal;
using Serilog;
using Serilog.Context;

namespace Chronicle.Serilog
{
    public class SerilogLogger : ILogger
    {
        [ThreadStatic] static Stack<object> _scopeStack;
        readonly Dictionary<LogSeverityLevel, LogWriterProvider> _map;
        readonly global::Serilog.ILogger _logger;

        delegate void LogWriterProvider(LogEntry logEntry);

        public string ScopeDelimiter { get; set; } = ":";

        public SerilogLogger(Type type)
        {
            _logger = Log.ForContext(type);

            _map = new Dictionary<LogSeverityLevel, LogWriterProvider>
            {
                {LogSeverityLevel.Verbose, e => _logger.Debug(e.ToString())},
                {LogSeverityLevel.Debug, e => _logger.Debug(e.ToString())},
                {LogSeverityLevel.Information, e => _logger.Information(e.ToString())},
                {LogSeverityLevel.Warning, e => _logger.Warning(e.ToString())},
                {LogSeverityLevel.Error, e => _logger.Error(e.ToString())},
                {LogSeverityLevel.Critical, e => _logger.Fatal(e.ToString())}
            };
        }

        public void Write(LogEntry logEntry)
        {
            var traceEventType = logEntry.Severity;

            if (_map.ContainsKey(traceEventType))
                _map[traceEventType](logEntry);
        }

        public bool IsEnabled(LogSeverityLevel severity)
        {
            return true;
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            if (_scopeStack == null)
                _scopeStack = new Stack<object>();
            var stackItem = new DisposableStackItem(_scopeStack, state);

            var logContext = LogContext.PushProperty("Scope", GetScope());

            return new ComposableDisposable(logContext, stackItem);
        }

        string GetScope()
        {
            if (_scopeStack != null && _scopeStack.Any())
                return string.Join(ScopeDelimiter, _scopeStack.Reverse().ToArray());

            return string.Empty;
        }
    }
}