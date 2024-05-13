using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebApp.Data;
using RazorPages.WebApp.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;



namespace WebApp.Pages
{
    [Authorize]
    public class JoinedChallengesModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public JoinedChallengesModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<JoinedChallenges> JoinedChallenges { get; set; }

        public async Task OnGetAsync()
        {
            JoinedChallenges = await _context.JoinedChallenges
                .Include(j => j.Challenge)
                .ToListAsync();
        }

        [BindProperty]
        public int ChallengeId { get; set; }

        public async Task<IActionResult> OnPostLeaveChallengeAsync()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userId == null)
            {
                // If the user ID is not found, redirect to an error page
                return RedirectToPage("/Error");
            }

            // Find the joined challenge by challengeId and userId
            var joinedChallenge = await _context.JoinedChallenges
                .FirstOrDefaultAsync(j => j.ChallengeId == ChallengeId && j.UserId == userId);

            if (joinedChallenge != null)
            {
                // Remove the joined challenge from the database
                _context.JoinedChallenges.Remove(joinedChallenge);
                await _context.SaveChangesAsync();

                // Set a success message
                TempData["SuccessMessage"] = "You have successfully left the challenge.";
            }
            else
            {
                // Set an error message if the joined challenge was not found
                TempData["ErrorMessage"] = "Failed to leave the challenge.";
            }

            // Redirect back to the JoinedChallenges page
            return RedirectToPage("./JoinedChallenges");
        }
    }
}
