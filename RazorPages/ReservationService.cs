public class ReservationService : IReservationService
{
    private readonly ReservationHandler _reservationHandler;
    private readonly ReservationRepository _reservationRepository;
    private readonly LogHandler _logHandler;
    public RoomHandler RoomHandler { get; }


    public ReservationService(ReservationHandler reservationHandler, LogHandler logHandler, RoomHandler roomHandler)
    {
        _reservationHandler = reservationHandler;
        _logHandler = logHandler;
        RoomHandler = roomHandler;

    }

        public bool AddReservation(Reservation reservation, string reserverName, DateTime chosenDateTime)
    {
        if (_reservationHandler.AddReservation(reservation, reserverName, chosenDateTime))
        {
            Console.WriteLine("\nReservation added successfully.\n");
            return true; // Return true to indicate successful 
        }
        else
        {
            return false; // if reservation addition fails
        }
    }

        public List<Reservation> GetReservationsForRoom(string roomId)
    {
        return _reservationHandler.GetReservationsForRoom(roomId);
    }

    public void DeleteReservation(Reservation reservation, string reserverName)
    {
        try
        {
            _reservationHandler.DeleteReservation(reservation, reserverName);
            Console.WriteLine("\nReservation deleted successfully.\n");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred while deleting reservation: {ex.Message}");
        }
    }


    public void DisplayWeeklySchedule()
    {
        _reservationHandler.DisplayScheduleForWeek();
    }

        public List<Reservation> DisplayReservationByReserver(string name)
    {
        return _reservationHandler.GetReservationsByReserver(name);
    }

        public List<Reservation> DisplayReservationByRoomId(string roomId)
    {
        return _reservationHandler.GetReservationsByRoomId(roomId);
    }



}