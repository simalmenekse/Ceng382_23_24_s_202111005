using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPages.Models; 

namespace RazorPages.Pages
{
    public class FormPageModel : PageModel
    {
        private readonly AppDbContext _dbContext;

        public FormPageModel(AppDbContext dbContext)
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
