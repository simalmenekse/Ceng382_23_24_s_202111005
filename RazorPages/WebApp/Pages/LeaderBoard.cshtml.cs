using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RazorPages.WebApp.Models;
using WebApp.Data;

namespace WebApp.Pages
{
    public class LeaderboardModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public LeaderboardModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<UserLeaderboardViewModel> Leaderboard { get; set; }

        public async Task OnGetAsync()
        {
            // Get user IDs, their completed challenges count, and profile picture URLs
            var leaderboardData = await _context.JoinedChallenges
                .Where(j => j.User.ProfilePicture != null) // Ensure user has a profile picture
                .GroupBy(j => j.UserId)
                .Select(g => new UserLeaderboardViewModel
                {
                    UserId = g.Key,
                    UserName = GetUserNameFromEmail(g.FirstOrDefault().User.Email), // Extract username from email
                    ProfilePicture = g.FirstOrDefault().User.ProfilePicture, // Assign byte[] directly
                    CompletedChallengesCount = g.Count(j => j.IsCompleted)
                })
                .OrderByDescending(u => u.CompletedChallengesCount)
                .ToListAsync();

            // Convert byte[] profile picture to URL string
            foreach (var user in leaderboardData)
            {
                user.ProfilePictureUrl = $"data:image/jpeg;base64,{Convert.ToBase64String(user.ProfilePicture)}";
            }

            Leaderboard = leaderboardData;
        }

        private static string GetUserNameFromEmail(string email)
        {
            // Find the index of the "@" symbol
            int atIndex = email.IndexOf('@');

            // Extract the substring before the "@" symbol
            string userName = email.Substring(0, atIndex);

            return userName;
        }
    }

    public class UserLeaderboardViewModel
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public byte[] ProfilePicture { get; set; } // Store byte[] directly
        public string ProfilePictureUrl { get; set; } // Store URL string
        public int CompletedChallengesCount { get; set; }
    }
}
