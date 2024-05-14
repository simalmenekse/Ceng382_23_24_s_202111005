
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RazorPages.WebApp.Models
{
    public class Challenge
    {
        
        public int Id { get; set; }
        public string Category { get; set; }
        public string Period { get; set; }
        public string DifficultyLevel { get; set; }
        public string Instructions { get; set; }

        public List<Comment> Comments { get; set; }


    }

}