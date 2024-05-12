using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApp.Data;
using RazorPages.WebApp.Models;
using Microsoft.AspNetCore.Authorization;

namespace WebApp.Pages
{
    [Authorize]
    public class AddChallengeModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        [BindProperty]
        public Challenge Challenge { get; set; }

        public AddChallengeModel(ApplicationDbContext context)
        {
            _context = context; 
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (Challenge != null)
            {
                _context.Challenges.Add(Challenge);
                _context.SaveChanges();
                ViewData["SuccessMessage"] = "Challenge successfully added!";
            }
            else
            {
                ViewData["ErrorMessage"] = "Challenge object is null.";
            }

            return Page();
        }
    }
}
