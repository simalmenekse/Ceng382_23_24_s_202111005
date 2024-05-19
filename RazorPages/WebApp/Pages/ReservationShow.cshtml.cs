using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RazorPages.WebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Data;

namespace WebApp.Pages
{
    public class ReservationShowModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public ReservationShowModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Reservation> Reservations { get; set; }

        // Filters
        public string RoomFilter { get; set; }
        public DateTime? StartDateFilter { get; set; }
        public DateTime? EndDateFilter { get; set; }
        public int? CapacityFilter { get; set; }

        public async Task OnGetAsync(string roomFilter, DateTime? startDateFilter, DateTime? endDateFilter, int? capacityFilter)
        {
            RoomFilter = roomFilter;
            StartDateFilter = startDateFilter;
            EndDateFilter = endDateFilter;
            CapacityFilter = capacityFilter;

            IQueryable<Reservation> query = _context.Reservations.Include(r => r.Room);

            if (!string.IsNullOrEmpty(RoomFilter))
            {
                query = query.Where(r => r.Room.RoomName.Contains(RoomFilter));
            }
            if (StartDateFilter.HasValue)
            {
                query = query.Where(r => r.DateTime >= StartDateFilter.Value);
            }
            if (EndDateFilter.HasValue)
            {
                query = query.Where(r => r.DateTime <= EndDateFilter.Value);
            }
            if (CapacityFilter.HasValue)
            {
                query = query.Where(r => r.Room.Capacity >= CapacityFilter.Value);
            }

            Reservations = await query.ToListAsync();
        }

        public IActionResult OnGetEdit(int id)
        {
            return RedirectToPage("/Reservations/Edit", new { id });
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id, string confirmEmail)
        {
            var reservation = await _context.Reservations.FindAsync(id);

            if (reservation == null)
            {
                return NotFound();
            }

            _context.Reservations.Remove(reservation);
            await _context.SaveChangesAsync();

            return RedirectToPage();
        }

        public string GetFriendlyName(string email)
        {
            var atIndex = email.IndexOf('@');
            if (atIndex > 0)
            {
                return email.Substring(0, atIndex);
            }
            return email;
        }
    }
}
