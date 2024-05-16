
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc;


namespace RazorPages.WebApp.Models
{
    public class Challenge
    {
        
        public int Id { get; set; }
        public string Category { get; set; }
        public string Period { get; set; }
        public string DifficultyLevel { get; set; }
        public string Instructions { get; set; }
    }
}