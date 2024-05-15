using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApp.Pages
{
    public class FaqItem
    {
        public int Id { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
    }

    [Authorize]
    public class FaqsModel : PageModel
    {
        public List<FaqItem> FaqItems { get; set; }

        public void OnGet()
        {

        }
    }
}
