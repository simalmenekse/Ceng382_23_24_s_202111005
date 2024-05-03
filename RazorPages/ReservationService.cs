using System;
using RazorPages.Models;
public class ReservationService
{
    private readonly AppDbContext _dbContext;

    public ReservationService(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

public void AddReservation(Room room, DateTime dateTime, string reservedBy)
{
    var reservation = new Reservation
    {
        Room = room,
        DateTime = dateTime,
        ReservedBy = reservedBy
    };

    _dbContext.Reservations.Add(reservation);
    _dbContext.SaveChanges();
}

}
