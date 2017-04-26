using System;

namespace Chronicle
{
    public static class LoggerExtensions
    {
        public static void Write(this ILogger logger, string message)
        {
            logger.Write(new LogEntry(message, LogSeverityLevel.Information));
        }

        public static void Write(this ILogger logger, string message, LogSeverityLevel logSeverityLevel)
        {
            logger.Write(new LogEntry(message, logSeverityLevel));
        }

        public static void Write(this ILogger logger, string message, Exception exception)
        {
            logger.Write(new LogEntry($"{message}: {exception}", LogSeverityLevel.Error));
        }
    }
}