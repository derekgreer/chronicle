# Chronicle
Chronicle is a logging abstraction library.

# Quickstart
## Installation
```
$> nuget install Chronicle
```

## Initialization
```
LogManager.SetLoggerProvider(categoryName => /* return an ILogger implementation */);
```

## Logging

```
using Chronicle;

namespace MyCompany.Application.Initialization
{
    public class SomeService
    {
        readonly ILogger _logger = LogManager.GetLogger<LoggingApplicationInitializer>();

        public void DoSomething()
        {
            _logger.Write("Log some information.");
        }
    }
}
```

