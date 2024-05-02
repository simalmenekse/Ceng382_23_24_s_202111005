using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
public class IndexModel : PageModel
{
        private readonly ReservationService _reservationService;

    private readonly ILogger<IndexModel> _logger;

    public IndexModel(ILogger<IndexModel> logger, ReservationService reservationService)
    {
        _logger = logger;
                _reservationService = reservationService;

    }

    public void OnGet()
    {
        // Sample values for demonstration purposes
        Room room = new Room
        {
            Id = 1,
            RoomName = "Sample Room",
            Capacity = 10
        };
        DateTime dateTime = DateTime.Now; // Replace this with the desired date and time
        string reservedBy = "John Doe"; // Replace this with the desired name

        // Call the AddReservation method of ReservationService
        _reservationService.AddReservation(room, dateTime, reservedBy);

    }

}
