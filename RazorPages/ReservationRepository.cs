public class ReservationRepository : IReservationRepository
{
    private readonly List<Reservation> _reservations;

    public ReservationRepository()
    {
        _reservations = new List<Reservation>();
    }

public void AddReservation(Reservation reservation)
{
    _reservations.Add(reservation);
}


public bool DeleteReservation(Reservation reservation)
{
    var reservationToDelete = _reservations.FirstOrDefault(r => r.DateTime == reservation.DateTime && r.Room.RoomId == reservation.Room.RoomId);
    if (reservationToDelete != null) // Corrected variable
    {
        // Remove the reservation from the list
        _reservations.Remove(reservationToDelete);
        return true;
    }
    else
    {
        Console.WriteLine("Reservation not found.");
        return false;
    }
}

    public List<Reservation> GetAllReservations()
    {
        return _reservations.ToList(); 
    }
}
