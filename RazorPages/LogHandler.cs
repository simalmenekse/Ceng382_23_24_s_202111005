using System.IO;
using System.Text.Json;

// ... other using directives

public class LogHandler
{
    private readonly ILogger _logger;
    private readonly string _logFilePath;


    public LogHandler(ILogger logger)
    {
        _logger = logger;
    }

    public void AddLog(LogRecord log)
    {
        _logger.Log(log); // Now it accepts a LogRecord object
    }
}
