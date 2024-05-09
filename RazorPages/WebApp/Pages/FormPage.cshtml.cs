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

        public FormPageModel(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
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

            return RedirectToPage("/Index"); 
        }
    }
}