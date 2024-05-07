using System;
using System.IO;
using System.Text.Json;
using System.Collections.Generic;

public class LogHandler
{
    private readonly ILogger _logger;
    private readonly IReservationRepository _reservationRepository;
    private readonly string _reservationDataFilePath;
        private List<LogRecord> _logs;


    public LogHandler(ILogger logger, IReservationRepository reservationRepository)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _reservationRepository = reservationRepository ?? throw new ArgumentNullException(nameof(reservationRepository));
        _reservationDataFilePath = "ReservationData.json";
                _logs = new List<LogRecord>(); // Initialize the list of logs

    }

    public void AddLog(LogRecord logRecord)
    {
        if (logRecord == null)
        {
            throw new ArgumentNullException(nameof(logRecord));
        }

        _logger.Log(logRecord);
    }
public void LogReservationsToFile(Reservation reservation)
{
    try
    {
        if (reservation != null)
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(reservation, options);

            // Check if the file already exists
            if (File.Exists(_reservationDataFilePath))
            {
                // Read existing content
                string existingJson = File.ReadAllText(_reservationDataFilePath);

                // Remove the closing bracket from existing content
                existingJson = existingJson.TrimEnd(']').Trim();

                // Append the reservation JSON preceded by a comma and a newline
                File.WriteAllText(_reservationDataFilePath, existingJson + "," + Environment.NewLine + json + "]");
            }
            else
            {
                // Write the reservation JSON to a new file
                File.WriteAllText(_reservationDataFilePath, "[" + Environment.NewLine + json + "]");
            }
        }
        else
        {
            Console.WriteLine("No reservation provided to log.");
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error logging reservation: {ex.Message}");
    }
}

    public List<LogRecord> GetAllLogs()
    {
        return _logs;
    }




}
