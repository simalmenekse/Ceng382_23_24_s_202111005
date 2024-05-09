using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using RazorPages.WebApp.Models;
using WebApp.Data;
using Microsoft.AspNetCore.Authorization;


namespace WebApp.Pages
{

[Authorize]
public class RoomsModel : PageModel
{
    private readonly ApplicationDbContext _dbContext;

    public RoomsModel(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public IList<Room> Rooms { get; set; }

    public async Task OnGetAsync()
    {
        Rooms = await _dbContext.Rooms.ToListAsync();
    }

    
}
}