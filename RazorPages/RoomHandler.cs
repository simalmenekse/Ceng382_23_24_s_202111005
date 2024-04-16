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
private readonly IReservationRepository _reservationRepository;

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
            throw; 
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
            throw; 
        }
    }

public List<DateTime> GetRoomTimeSlots(string roomId)
{

    List<DateTime> timeSlots = new List<DateTime>();

    DateTime currentDate = DateTime.Today.Date; 

    // Generate time slots from 9 am to 6 pm
    for (int i = 9; i <= 18; i++)
    {
        timeSlots.Add(currentDate.AddHours(i));
    }

    return timeSlots;
}
    public Dictionary<string, List<DateTime>> GenerateTimeSlotsForRooms(List<Room> rooms)
    {
        Dictionary<string, List<DateTime>> roomTimeSlots = new Dictionary<string, List<DateTime>>();

        foreach (var room in rooms)
        {
            List<DateTime> timeSlots = new List<DateTime>();
            DateTime currentDate = DateTime.Today.Date; // Start from today and set the time to 9 am

            // Start generating time slots from 9 am to 6 pm
            for (int i = 9; i <= 18; i++)
            {
                timeSlots.Add(currentDate.AddHours(i));
            }

            // Associate the generated time slots with the room ID
            roomTimeSlots.Add(room.RoomId, timeSlots);
        }

        return roomTimeSlots;
    }


        public void ShowRoomCapacities(List<Room> rooms)
    {
        Console.WriteLine("\n-------------------------------------------------------");
        Console.WriteLine("| Room ID | Room Name     | Remaining Capacity |");
        Console.WriteLine("-------------------------------------------------------");

        foreach (var room in rooms)
        {
            int existingReservations = _reservationRepository.GetAllReservations().Count(r => r.Room.RoomId == room.RoomId);
            int remainingCapacity = room.Capacity - existingReservations;

            Console.WriteLine($"| {room.RoomId,-7} | {room.RoomName,-13} | {remainingCapacity,-18} |");
        }

        Console.WriteLine("-------------------------------------------------------\n");
    }
}