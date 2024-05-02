using System;
public class ReservationService
{
    private readonly AppDbContext _dbContext;

    public ReservationService(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

public void AddReservation(Room room, DateTime dateTime, string reservedBy)
{
    // Create a new reservation object
    var reservation = new Reservation
    {
        Room = room,
        DateTime = dateTime,
        ReservedBy = reservedBy
    };

    // Add the reservation to the context and save changes
    _dbContext.Reservations.Add(reservation);
    _dbContext.SaveChanges();
}

}
