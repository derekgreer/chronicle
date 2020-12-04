using System;

namespace Chronicle
{
    public static class LogManager
    {
        public static ILogger GetLogger<T>()
        {
            return Context.LogContext.LoggerProvider(typeof(T));
        }

        public static ILogger GetLogger(Type type)
        {
            return Context.LogContext.LoggerProvider(type);
        }
    }
}