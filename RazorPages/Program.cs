
using System;
using System.IO;
using System.Text.Json;
using System.Collections.Generic;
using System.Text.Json.Serialization;

/*
1)First Question: why the code from last week fails to satisfy Single Responsibility and Dependency Injection:

- Single Responsibility Principle (SRP):
The SRP suggests that a class should have only one reason to change. Each class should handle only one responsibility or should encapsulate only one aspect of the functionality.
    * ReservationHandler class handles too many responsibilities. It manages the reservations, counts reservations, displays schedules, and shows room capacities.
    * ReservationHandler should only manage reservations and  adding, deleting, and counting reservations. ReservationHandler is also handling input operations.

- Dependency Injection Principle:
Dependency Injection (DI) is a technique in which an object receives other objects that it depends on (dependencies) rather than creating them itself.
    * ReservationHandler creates instances of Room and Reservation, violating the Dependency Injection Principle.

- Why is it essential to use these principles in web applications?
    * Maintainability: By following SRP, code becomes more maintainable and each class has single responsibility, making it easier to understand and modify.
    * Testability: DI helps in writing testable code. You can easilyreplace them during testing.
    * Scalability: Following these, ensures that your code become flexible and scalable. When new features or changes are required, it's easier to modify the code.
    * Collaboration: Proper separation of responsibilities and dependency makes it easy for multiple programmers to work together on a project. Each developer may work on different parts at the same time.
 */
class Program
{
    static void Main(string[] args)
    {
        const string roomDataFilePath = "Data.json";
        const string logDataFilePath = "LogData.json";

        var roomHandler = new RoomHandler(roomDataFilePath);
        var reservationRepository = new ReservationRepository();
        var logger = new FileLogger(logDataFilePath);
        var logHandler = new LogHandler(logger);
        var reservationHandler = new ReservationHandler(reservationRepository, roomHandler, logHandler);
        var reservationService = new ReservationService(reservationHandler, logHandler, roomHandler);

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
                    AddReservation(reservationService, roomHandler);
                    break;
                case 2:
                    DeleteReservation(reservationService);
                    break;
                case 3:
                    reservationService.DisplayWeeklySchedule();
                    break;
                case 4:
                    var rooms = roomHandler.GetRooms();
                    try
                    {
                        reservationHandler.ShowAvailableRooms(rooms);
                    }
                    catch (JsonException ex)
                    {
                        Console.WriteLine($"Error: Invalid JSON format in 'data.json'. {ex.Message}");
                    }
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

    static void AddReservation(ReservationService reservationService, RoomHandler roomHandler)
    {
        string roomId = GetValidRoomId();

        Console.WriteLine("\nSelect Reservation Date and Time Slot:");
        var availableSlots = roomHandler.GetRoomTimeSlots(roomId);
        for (int i = 0; i < availableSlots.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {availableSlots[i].ToString("yyyy-MM-dd HH:mm")}");
        }

        int slotIndex;
        if (!int.TryParse(Console.ReadLine(), out slotIndex) || slotIndex < 1 || slotIndex > availableSlots.Count)
        {
            Console.WriteLine("Invalid choice. Please select a valid slot index.");
            return;
        }

        var chosenDateTime = availableSlots[slotIndex - 1];

        Console.WriteLine("\nEnter Name of Person Making Reservation:");
        string name = Console.ReadLine();

        var room = new Room { RoomId = roomId };
        var reservation = new Reservation(room, chosenDateTime, name);
        reservationService.AddReservation(reservation, name, chosenDateTime);
    }

    static void DeleteReservation(ReservationService reservationService)
    {
        string roomId = GetValidRoomId();

        Console.WriteLine("\nSelect Reservation Date and Time Slot to Delete:");
        var reservationsForRoom = reservationService.GetReservationsForRoom(roomId);
        for (int i = 0; i < reservationsForRoom.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {reservationsForRoom[i].DateTime.ToString("yyyy-MM-dd HH:mm")} - Reserved by: {reservationsForRoom[i].ReservedBy}");
        }

        int reservationIndex;
        if (!int.TryParse(Console.ReadLine(), out reservationIndex) || reservationIndex < 1 || reservationIndex > reservationsForRoom.Count)
        {
            Console.WriteLine("Invalid choice. Please select a valid reservation index.");
            return;
        }

        var chosenReservation = reservationsForRoom[reservationIndex - 1];

        Console.WriteLine("\nEnter Your Name for Verification:");
        string name = Console.ReadLine();

        if (chosenReservation.ReservedBy != name)
        {
            Console.WriteLine("Name does not match the reservation. Delete operation aborted.");
            return;
        }

        reservationService.DeleteReservation(chosenReservation, name);
    }


    static string GetValidRoomId()
    {
        Console.WriteLine("Enter Room ID:");
        string roomId;
        do
        {
            roomId = Console.ReadLine();
            if (string.IsNullOrEmpty(roomId))
            {
                Console.WriteLine("Room ID cannot be empty. Please enter a valid ID:");
            }
        } while (string.IsNullOrEmpty(roomId));
        return roomId;
    }
}
