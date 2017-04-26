namespace Chronicle
{
    public class LogEntry
    {
        public LogEntry(string message) : this(message, (LogSeverityLevel) LogSeverityLevel.Information)
        { }

        public LogEntry(string message, LogSeverityLevel severity)
        {
            Message = message;
            Severity = severity;
        }

        public string Message { get; set; }

        public LogSeverityLevel Severity { get; set; }

        public override string ToString()
        {
            return $"{Message}";
        }
    }
}