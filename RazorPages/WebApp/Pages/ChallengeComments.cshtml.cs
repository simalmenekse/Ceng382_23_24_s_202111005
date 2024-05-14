using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebApp.Data;
using RazorPages.WebApp.Models;

namespace WebApp.Pages
{
    public class ChallengeCommentsModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public ChallengeCommentsModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public Challenge Challenge { get; set; }
        public List<Comment> Comments { get; set; }
        public double AverageRating { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Challenge = await _context.Challenges.FindAsync(id);

            if (Challenge == null)
            {
                return NotFound();
            }

            Comments = await _context.Comments
                .Where(c => c.ChallengeId == id)
                .Include(c => c.User)
                .ToListAsync();

            if (Comments.Any())
            {
                AverageRating = Comments.Average(c => c.Rating);
            }

            return Page();
        }
    }
}
