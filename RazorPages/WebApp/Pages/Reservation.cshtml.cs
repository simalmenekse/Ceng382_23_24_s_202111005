using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApp.Data;
using RazorPages.WebApp.Models;

namespace WebApp.Pages
{
    public class ReservationModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        [BindProperty]
        public Reservation Reservation { get; set; }

        public SelectList Rooms { get; set; }

        public ReservationModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public void OnGet()
        {
            Rooms = new SelectList(_context.Rooms, "RoomName", "RoomName");
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                Rooms = new SelectList(_context.Rooms, "RoomName", "RoomName");
                return Page();
            }

            Reservation.ReservedBy = User.Identity.Name;

            Reservation.Room = await _context.Rooms.FirstOrDefaultAsync(r => r.RoomName == Reservation.Room.RoomName);

            if (Reservation.Room == null)
            {
                ModelState.AddModelError("Reservation.Room.RoomName", "Invalid room selection.");
                Rooms = new SelectList(_context.Rooms, "RoomName", "RoomName");
                return Page();
            }

            _context.Reservations.Add(Reservation);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
