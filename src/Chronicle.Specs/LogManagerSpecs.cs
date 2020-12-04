using System.Threading;
using Chronicle.Configuration;
using Chronicle.Serilog;
using ExpectedObjects;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace Chronicle.Specs
{
    public class LogManagerSpecs
    {
        [Subject("Logger")]
        public class when_creating_a_logger_without_setting_the_provider
        {
            static ILogger _logger;

            Because of = () => _logger = LogManager.GetLogger<LogManagerSpecs>();

            It should_create_a_null_logger = () => _logger.GetType().Name.ShouldEqual("NullLogger");
        }

        [Subject("Logger")]
        public class when_logging_with_a_created_logger
        {
            const string Id = "D828E6F6-7536-4375-B84E-ECB566D0686F";
            static ILogger _logger;
            static LogEntry _logEntry;

            Establish context = () =>
            {
                var loggerStub = new Mock<ILogger>();
                loggerStub.Setup(x => x.Write(Moq.It.IsAny<LogEntry>())).Callback<LogEntry>(e => _logEntry = e);
                LoggingConfiguration.Configure(ctx => ctx.SetLoggerProvider(t => loggerStub.Object));
                _logger = LogManager.GetLogger<LogManagerSpecs>();
            };

            Because of = () => _logger.Write(new LogEntry(Id));

            It should_log = () => _logEntry.Message.ShouldEqual(Id);
        }

        [Subject("Logger")]
        public class when_logging_with_a_created_logger_non_generic
        {
            const string Id = "D828E6F6-7536-4375-B84E-ECB566D0686F";
            static ILogger _logger;
            static LogEntry _logEntry;

            Establish context = () =>
            {
                var loggerStub = new Mock<ILogger>();
                loggerStub.Setup(x => x.Write(Moq.It.IsAny<LogEntry>())).Callback<LogEntry>(e => _logEntry = e);
                LoggingConfiguration.Configure(ctx => ctx.SetLoggerProvider(t => loggerStub.Object));
                _logger = LogManager.GetLogger(typeof(LogManagerSpecs));
            };

            Because of = () => _logger.Write(new LogEntry(Id));

            It should_log = () => _logEntry.Message.ShouldEqual(Id);
        }
    }
}