using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApp.Data;
using RazorPages.WebApp.Models;
using Microsoft.AspNetCore.Authorization;

namespace WebApp.Pages
{
    [Authorize]
    public class AddChallengeModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        [BindProperty]
        public Challenge Challenge { get; set; }

        public AddChallengeModel(ApplicationDbContext context)
        {
            _context = context; 
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        public IActionResult OnPost()
{
    if (!ModelState.IsValid)
    {
        // Log ModelState errors
        foreach (var modelState in ModelState.Values)
        {
            foreach (var error in modelState.Errors)
            {
                Console.WriteLine(error.ErrorMessage);
            }
        }
        return Page();
    }

    try
    {
        // Log Challenge object to check if it's properly populated
        Console.WriteLine($"Category: {Challenge.Category}");
        Console.WriteLine($"Period: {Challenge.Period}");
        Console.WriteLine($"Difficulty Level: {Challenge.DifficultyLevel}");
        Console.WriteLine($"Instructions: {Challenge.Instructions}");
        
        if (Challenge != null)
        {
            _context.Challenges.Add(Challenge);
            _context.SaveChanges();
            ViewData["SuccessMessage"] = "Challenge successfully added!";
        }
        else
        {
            ViewData["ErrorMessage"] = "Challenge object is null.";
        }
    }
    catch (Exception ex)
    {
        // Log any exceptions that occur during save operation
        Console.WriteLine($"Exception: {ex.Message}");
        ViewData["ErrorMessage"] = "An error occurred while adding the challenge. Please try again later.";
    }

    return Page();
}
    }
}
