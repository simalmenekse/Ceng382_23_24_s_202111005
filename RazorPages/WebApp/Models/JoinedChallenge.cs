using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RazorPages.WebApp.Models
{
    public class JoinedChallenges
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Challenge")]
        public int ChallengeId { get; set; }
        public Challenge Challenge { get; set; }

        [ForeignKey("User")]
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        public DateTime JoinDate { get; set; }
        public bool IsFavorite { get; set; }
        public bool IsCompleted { get; set; }
        public int CompletedChallengesCount { get; set; }

    }
}
