using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPages.WebApp.Models;
using WebApp.Data;
using Microsoft.AspNetCore.Authorization;


namespace WebApp.Pages
{
    [Authorize]
    public class AboutPageModel : PageModel
    {
        private readonly ApplicationDbContext _dbContext;

        public AboutPageModel(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult OnGet()
        {
            return Page();
        }
    }
}