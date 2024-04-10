using System;
using System.IO;
using System.Text.Json;
using System.Collections.Generic;
using System.Text.Json.Serialization;

public class RoomData
{
    public List<Room> Room { get; set; }
}

public class RoomHandler
{
    private readonly string _filePath;

    public RoomHandler(string filePath)
    {
        _filePath = filePath;
    }

    public List<Room> GetRooms()
    {
        try
        {
            var jsonString = File.ReadAllText(_filePath);
            var options = new JsonSerializerOptions()
            {
            NumberHandling = JsonNumberHandling.AllowReadingFromString | JsonNumberHandling.WriteAsString
            };
            var roomData = JsonSerializer.Deserialize<RoomData>(jsonString, options);
            return roomData.Room;
        }
        catch (JsonException ex)
        {
            Console.WriteLine($"Error deserializing JSON: {ex.Message}");
            throw; // Rethrow the exception to indicate failure
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
            throw; // Rethrow the exception to indicate failure
        }
    }

public List<DateTime> GetRoomTimeSlots(string roomId)
{
    // Implement logic to retrieve time slots for the given room
    // For this example, let's generate time slots from 9 am to 6 pm for each day

    List<DateTime> timeSlots = new List<DateTime>();

    DateTime currentDate = DateTime.Today.Date; // Start from today and set the time to 9 am

    // Generate time slots from 9 am to 6 pm
    for (int i = 9; i <= 18; i++)
    {
        // Add the current hour to the time slots list
        timeSlots.Add(currentDate.AddHours(i));
    }

    return timeSlots;
}


    public void SaveRooms(List<Room> rooms)
    {
        try
        {
            var roomData = new RoomData { Room = rooms };
            var jsonString = JsonSerializer.Serialize(roomData);
            File.WriteAllText(_filePath, jsonString);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
            throw; // Rethrow the exception to indicate failure
        }
    }
}