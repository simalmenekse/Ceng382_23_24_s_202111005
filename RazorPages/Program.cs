using System;
using System.IO;
using System.Text.Json;
using System.Collections.Generic;
using System.Text.Json.Serialization;

public class RoomData
{
    [JsonPropertyName("Room")]
    public Room[]? Rooms { get; set; }
}

public class Room
{
    [JsonPropertyName("roomId")]
    public string? RoomId { get; set; }

    [JsonPropertyName("roomName")]
    public string? RoomName { get; set; }

    [JsonPropertyName("capacity")]
    public int Capacity { get; set; }
}

public class Reservation
{
    public Room Room { get; set; }
    public DateTime DateTime { get; set; }
    public string ReservedBy { get; set; }

    public Reservation(Room room, DateTime dateTime, string reservedBy)
    {
        Room = room;
        DateTime = dateTime;
        ReservedBy = reservedBy;
    }

    public override string ToString()
    {
        return $"Room: {Room.RoomId} - {Room.RoomName}, Date: {DateTime.ToShortDateString()}, Time: {DateTime.ToShortTimeString()}, Reserved By: {ReservedBy}";
    }
}
public class ReservationHandler
{
    private Reservation[,] schedule;

    public ReservationHandler(int daysOfWeek, int numberOfRooms)
    {
        schedule = new Reservation[daysOfWeek, numberOfRooms];
    }

    public bool AddReservation(Reservation reservation)
    {
        if (reservation.Room.RoomId != null)
        {
            int dayIndex = (int)reservation.DateTime.DayOfWeek;
            int roomIndex = int.Parse(reservation.Room.RoomId) - 1;

            if (schedule[dayIndex, roomIndex] == null)
            {
                schedule[dayIndex, roomIndex] = reservation;
                return true;
            }
            else
            {
                Console.WriteLine("There is already a reservation for that room at that time. Please choose another time slot.");
                return false;
            }
        }
        else
        {
            Console.WriteLine("Room ID is null.");
            return false;
        }
    }

    public int CountReservationsForRoom(Room room)
    {
        int count = 0;
        foreach (var reservation in schedule)
        {
            if (reservation != null && reservation.Room.RoomId == room.RoomId)
            {
                count++;
            }
        }
        return count;
    }

    public void ShowRoomCapacities(RoomData roomData, ReservationHandler handler)
    {
        Console.WriteLine("\n-------------------------------------------------------");
        Console.WriteLine("| Room ID | Room Name     | Remaining Capacity |");
        if (roomData.Rooms != null)
        {
            foreach (var room in roomData.Rooms)
            {
                int existingReservations = handler.CountReservationsForRoom(room);
                int remainingCapacity = room.Capacity - existingReservations;

                Console.WriteLine($"| {room.RoomId,-7} | {room.RoomName,-13} | {remainingCapacity,-18} |");
            }
            Console.WriteLine("-------------------------------------------------------\n");
        }

    }


    public bool DeleteReservation(DateTime dateTime, Room room)
    {
        int dayIndex = (int)dateTime.DayOfWeek;

        if (room.RoomId != null)
        {
            int roomIndex = int.Parse(room.RoomId) - 1;

            if (schedule[dayIndex, roomIndex] != null && schedule[dayIndex, roomIndex].DateTime == dateTime)
            {
                schedule[dayIndex, roomIndex] = null;
                return true;
            }
            else
            {
                Console.WriteLine("No reservation found for the specified date and room.");
                return false;
            }
        }
        else
        {
            Console.WriteLine("Room ID is null.");
            return false;
        }
    }


    public void DisplayScheduleForWeek()
    {
        Console.WriteLine("\n-------------------------------------------------------");
        Console.WriteLine("|   Date     |   Time    |     Room    |  Reserved By |");
        Console.WriteLine("-------------------------------------------------------");

        foreach (var reservation in schedule)
        {
            if (reservation != null)
            {
                Console.WriteLine($"| {reservation.DateTime.ToShortDateString(),-10} | {reservation.DateTime.ToShortTimeString(),-8} | {reservation.Room.RoomName,-12} | {reservation.ReservedBy,-12} |");

            }
        }

        Console.WriteLine("-------------------------------------------------------\n");
    }

}

class Program
{
    static void Main(string[] args)
    {
        string jsonFilePath = "Data.json";

        string jsonString = File.ReadAllText(jsonFilePath);
        var options = new JsonSerializerOptions()
        {
            NumberHandling = JsonNumberHandling.AllowReadingFromString | JsonNumberHandling.WriteAsString
        };

        var roomData = JsonSerializer.Deserialize<RoomData>(jsonString, options);

        if (roomData?.Rooms != null)
        {
            ReservationHandler reservationHandler = new ReservationHandler(7, roomData.Rooms.Length);

            bool continueApp = true;
            while (continueApp)
            {
                Console.WriteLine("Choose an option:");
                Console.WriteLine("1. Add Reservation");
                Console.WriteLine("2. Delete Reservation");
                Console.WriteLine("3. Display This Week's Schedule");
                Console.WriteLine("4. Show Available Rooms");
                Console.WriteLine("5. Exit");

                int choice;
                if (!int.TryParse(Console.ReadLine(), out choice))
                {
                    Console.WriteLine("Wrong input. Please enter a number.");
                    continue;
                }

                switch (choice)
                {
                    case 1:
                        AddReservation(reservationHandler, roomData.Rooms);
                        break;
                    case 2:
                        DeleteReservation(reservationHandler);
                        break;
                    case 3:
                        reservationHandler.DisplayScheduleForWeek();
                        break;
                    case 4:
                        reservationHandler.ShowRoomCapacities(roomData, reservationHandler);
                        break;
                    case 5:
                        continueApp = false;
                        break;
                    default:
                        Console.WriteLine("Wrong option. Please choose a valid option.");
                        break;
                }
            }
        }
        else
        {
            Console.WriteLine("Failed to load room data from Json File.");
        }
    }

    static void AddReservation(ReservationHandler handler, Room[] rooms)
    {
        Console.WriteLine("Enter Room ID:");
        string? roomId = Console.ReadLine();

        Room? room = Array.Find(rooms, r => r.RoomId == roomId);
        if (room == null)
        {
            Console.WriteLine("Invalid Room ID.");
            return;
        }

        int existingReservations = handler.CountReservationsForRoom(room);

        if (existingReservations >= room.Capacity)
        {
            Console.WriteLine("Cannot add reservation. The room is at full capacity.");
            return;
        }

        Console.WriteLine("\nEnter Reservation Date and Time (yyyy-MM-dd HH:mm):");
        if (!DateTime.TryParse(Console.ReadLine(), out DateTime dateTime))
        {
            Console.WriteLine("Invalid date and time format.");
            return;
        }

        Console.WriteLine("\nEnter Name of Person Making Reservation:");
        string? name = Console.ReadLine();

        Reservation reservation = new Reservation(room, dateTime, name);
        if (handler.AddReservation(reservation))
        {
            Console.WriteLine("\nReservation added successfully.\n");
        }
    }

    static void DeleteReservation(ReservationHandler handler)
    {
        Console.WriteLine("\nEnter Room ID:");
        string? roomId = Console.ReadLine();

        Console.WriteLine("\nEnter Reservation Date and Time (yyyy-MM-dd HH:mm):");
        if (!DateTime.TryParse(Console.ReadLine(), out DateTime dateTime))
        {
            Console.WriteLine("Invalid date and time format.");
            return;
        }

        Room room = new Room { RoomId = roomId };

        if (handler.DeleteReservation(dateTime, room))
        {
            Console.WriteLine("\nReservation deleted successfully.\n");
        }
    }
}
