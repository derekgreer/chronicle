using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using log4net;

namespace Chronicle.Log4Net
{
    public class Log4NetLogger : ILogger
    {
        [ThreadStatic] static Stack<object> _scopeStack;
        readonly Dictionary<LogSeverityLevel, LogWriterProvider> _map;
        delegate void LogWriterProvider(LogEntry logEntry);

        public string ScopeDelimiter { get; set; } = ":";

        public Log4NetLogger(Type type)
        {
            var log = log4net.LogManager.GetLogger(type);

            _map =
                new Dictionary<LogSeverityLevel, LogWriterProvider>
                {
                    {LogSeverityLevel.Verbose, e => log.Info(e.ToString())},
                    {LogSeverityLevel.Debug, e => log.Debug(e.ToString())},
                    {LogSeverityLevel.Information, e => log.Info(e.ToString())},
                    {LogSeverityLevel.Warning, e => log.Warn(e.ToString())},
                    {LogSeverityLevel.Error, e => log.Error(e.ToString())},
                    {LogSeverityLevel.Critical, e => log.Fatal(e.ToString())}
                };
        }

        public void Write(LogEntry logEntry)
        {
            var traceEventType = logEntry.Severity;

            ThreadContext.Properties["scope"] = GetScope();
            ThreadContext.Properties["tid"] = Thread.CurrentThread.ManagedThreadId.ToString();

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
            return new DisposableStackItem(_scopeStack, state);
        }

        string GetScope()
        {
            if (_scopeStack != null && _scopeStack.Any())
                return string.Join(ScopeDelimiter, _scopeStack.Reverse().ToArray());

            return string.Empty;
        }
    }
}