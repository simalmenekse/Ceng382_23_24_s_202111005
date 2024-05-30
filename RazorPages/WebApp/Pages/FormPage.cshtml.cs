using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPages.WebApp.Models;
using WebApp.Data;
using Microsoft.AspNetCore.Authorization;


namespace WebApp.Pages
{
    [Authorize]
    public class FormPageModel : PageModel
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ILogger<FormPageModel> _logger;


        public FormPageModel(ApplicationDbContext dbContext, ILogger<FormPageModel> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        public IActionResult OnPost(Room room)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _dbContext.Rooms.Add(room);
            _dbContext.SaveChanges();

            string logMessage = $"Room {room.RoomName} added successfully.";

            _logger.LogInformation($"Room {room.RoomName} added successfully.");
                        SaveLogToDatabase(logMessage);


            return RedirectToPage("/Index"); 
        }

         private void SaveLogToDatabase(string message)
        {
            var logEntry = new LogEntry
            {
                Message = message,
                Timestamp = DateTime.UtcNow
            };

            _dbContext.LogEntries.Add(logEntry);
            _dbContext.SaveChanges();
        }
    }
}