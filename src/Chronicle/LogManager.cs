using System;

namespace Chronicle
{
    public static class LogManager
    {
        static Func<Type, ILogger> _provider = category => new NullLogger();

        public static void SetLoggerProvider(Func<Type, ILogger> provider)
        {
            _provider = provider;
        }

        public static ILogger GetLogger<T>()
        {
            return _provider(typeof(T));
        }
    }
}