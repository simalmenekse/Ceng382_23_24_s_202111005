using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RazorPages.WebApp.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using WebApp.Data;
using Microsoft.AspNetCore.Authorization;
using System;

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

        public string UserId { get; private set; }

        [BindProperty]
        public List<Challenge> Challenges { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            UserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            Challenges = await _context.Challenges.ToListAsync();

            return Page();
        }

        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OnPostJoinChallengeAsync(int challengeId)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToPage("/Account/Login", new { area = "Identity" });
            }

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                return Unauthorized();
            }

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

        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OnPostAddCommentAndRatingAsync(int challengeId, int rating, string content)
        {
            if (!ModelState.IsValid)
            {
                return Page(); 
            }

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                return Unauthorized();
            }

            var newComment = new Comment
            {
                UserId = userId,
                ChallengeId = challengeId,
                Rating = rating,
                Content = content,
                CreatedAt = DateTime.Now
            };

            _context.Comments.Add(newComment);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Browse");
        }

    }
}
