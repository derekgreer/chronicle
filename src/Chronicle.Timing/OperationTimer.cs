using System;
using System.Diagnostics;

namespace Chronicle.Timing
{
    public class OperationTimer : IDisposable
    {
        readonly string _actionName;
        readonly ILogger _logger;
        readonly IDisposable _scope;
        readonly Stopwatch _stopwatch;

        public OperationTimer(ILogger logger, string actionName)
        {
            _logger = logger;
            _scope = _logger.BeginScope(actionName);
            _actionName = actionName;
            _stopwatch = new Stopwatch();
            _stopwatch.Start();
            _logger.Write($"Executing operation {actionName}");
        }

        public void Dispose()
        {
            Dispose(true);
        }


        ~OperationTimer()
        {
            GC.SuppressFinalize(true);
        }

        public void Dispose(bool isDisposing)
        {
            _stopwatch.Stop();
            _logger.Write(
                $"Executed operation {_actionName} in {_stopwatch.Elapsed.Hours}:{_stopwatch.Elapsed.Minutes}:{_stopwatch.Elapsed.Seconds}.{_stopwatch.Elapsed.Milliseconds}");
            _scope?.Dispose();
        }
    }
}