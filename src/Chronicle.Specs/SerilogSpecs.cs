using System.Collections.Generic;
using System.Linq;
using Chronicle.Configuration;
using Chronicle.Serilog;
using Machine.Specifications;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.ListOfString;

namespace Chronicle.Specs
{
    public class SerilogSpecs
    {
        [Subject("Serilog")]
        public class when_logging_with_serilog
        {
            const string Id = "6ED99440-1B80-493C-8AED-D4DDB229C6D7";
            static IList<string> _actualLogEntries = new List<string>();
            static ILogger _logger;

            Establish context = () =>
            {
                Log.Logger = new LoggerConfiguration().MinimumLevel.Information().WriteTo.StringList(_actualLogEntries)
                    .CreateLogger();

                LoggingConfiguration.Configure(ctx => ctx.UseSerilog(config => config.MinimumLevel.Information().WriteTo.StringList(_actualLogEntries)));

                _logger = LogManager.GetLogger(typeof(LogManagerSpecs));
            };

            Because of = () => _logger.Write(new LogEntry(Id));

            It should_log = () => _actualLogEntries.FirstOrDefault().ShouldContain(Id);
        }

        [Subject("Serilog")]
        public class when_logging_with_serilog_using_scopes
        {
            const string Id = "A0E432E6-F063-4F46-A0D5-03725F6C9887";
            static IList<string> _actualLogEntries = new List<string>();
            static ILogger _logger;

            Establish context = () =>
            {
                LoggingConfiguration.Configure(ctx => ctx
                    .UseSerilog(config => config.Enrich.FromLogContext()
                        .MinimumLevel.Information().WriteTo.StringList(_actualLogEntries, LogEventLevel.Verbose,
                            outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj} {Properties:j}{NewLine}{Exception}" )));
                _logger = LogManager.GetLogger(typeof(LogManagerSpecs));
            };

            Because of = () =>
            {
                using(_logger.BeginScope("level1")) using (_logger.BeginScope("level2")) _logger.Write(new LogEntry(Id));
            };

            It should_log = () => _actualLogEntries.FirstOrDefault().ShouldContain("level1:level2");
        }
    }
}