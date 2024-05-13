using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RazorPages.WebApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Security.Claims;
using WebApp.Data;
using Microsoft.AspNetCore.Authorization;

namespace WebApp.Pages
{

    [Authorize]
    public class BrowseModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public BrowseModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public int ChallengeId { get; set; }

        public List<Challenge> Challenges { get; set; } // Add this property

        public async Task<IActionResult> OnGetAsync()
        {
            // Fetch the list of challenges
            Challenges = await _context.Challenges.ToListAsync();
            return Page();
        }
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OnPostJoinChallengeAsync(int challengeId)
        {
            if (!User.Identity.IsAuthenticated)
            {
                // If the user is not authenticated, redirect to the login page
                return RedirectToPage("/Account/Login", new { area = "Identity" });
            }

            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                // If the user's ID claim is not found, handle the error (log, return error response, etc.)
                // For now, just return a generic error page
                return RedirectToPage("/Error");
            }

            var userId = userIdClaim.Value;

            var joinedChallenge = new JoinedChallenges
            {
                UserId = userId,
                ChallengeId = challengeId,
                JoinDate = DateTime.Now
            };

            _context.JoinedChallenges.Add(joinedChallenge);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "You have successfully joined the challenge!";


            return RedirectToPage("./Browse"); 
        }

    }
}