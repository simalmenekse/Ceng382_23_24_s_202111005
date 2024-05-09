using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using RazorPages.WebApp.Models;
using WebApp.Data;
using Microsoft.AspNetCore.Authorization;


[Authorize]
public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;

    public IndexModel(ILogger<IndexModel> logger)
    {
        _logger = logger;

    }
    public void OnGet()
    {

    }

}