using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using WebApp.Data;
using RazorPages.WebApp.Models;

namespace WebApp.Pages.Reservations
{
    [Authorize]
    public class EditReservationModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<EditReservationModel> _logger;

        public EditReservationModel(ApplicationDbContext context, ILogger<EditReservationModel> logger)
        {
            _context = context;
            _logger = logger;
        }

        [BindProperty]
        public Reservation Reservation { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Reservation = await _context.Reservations.Include(r => r.Room).FirstOrDefaultAsync(m => m.Id == id);

            if (Reservation == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var reservationToUpdate = await _context.Reservations.FindAsync(Reservation.Id);

            if (reservationToUpdate == null)
            {
                return NotFound();
            }

            reservationToUpdate.DateTime = Reservation.DateTime;
            _context.Attach(reservationToUpdate).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                string logMessage = $"Reservation with ID {Reservation.Id} edited successfully.";
                _logger.LogInformation(logMessage);
                SaveLogToDatabase(logMessage);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReservationExists(Reservation.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("/ReservationShow");
        }

        private bool ReservationExists(int id)
        {
            return _context.Reservations.Any(e => e.Id == id);
        }

        private void SaveLogToDatabase(string message)
        {
            var logEntry = new LogEntry
            {
                Message = message,
                Timestamp = DateTime.UtcNow
            };

            _context.LogEntries.Add(logEntry);
            _context.SaveChanges();
        }
    }
}
