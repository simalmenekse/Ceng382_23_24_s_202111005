using System;
using System.Collections.Generic;
using System.Linq;

public class LogService
{
    private readonly List<LogRecord> _logRecords;

    public LogService(List<LogRecord> logRecords)
    {
        _logRecords = logRecords ?? throw new ArgumentNullException(nameof(logRecords));
    }

    public static List<LogRecord> DisplayLogsByName(List<LogRecord> logRecords, string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Name cannot be null or empty.", nameof(name));
        }

        return logRecords.Where(log => log.ReserverName == name).ToList();
    }

    public static List<LogRecord> DisplayLogs(List<LogRecord> logRecords, DateTime start, DateTime end)
    {
        return logRecords.Where(log => log.Timestamp >= start && log.Timestamp <= end).ToList();
    }
}
