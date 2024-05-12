using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RazorPages.WebApp.Models;
using WebApp.Data;

namespace WebApp.Pages
{
    public class BrowseModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public BrowseModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Challenge> Challenges { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            Challenges = await _context.Challenges.ToListAsync();
            return Page();
        }
    }
}
