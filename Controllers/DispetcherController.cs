using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TransportCompany.Data;

namespace TransportCompany.Controllers
{
    public class DispetcherController : Controller
    {
        private readonly TransportCompanyDbContext _context;
        public DispetcherController(TransportCompanyDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            ViewData["Dispetcher"] = _context.dispetchers.Where(a => a.Id == Convert.ToInt32(Request.Cookies["userId"])).ToList();
            return View();
        }

        public IActionResult ListTickets()
        {
            ViewData["Drivers"] = _context.drivers.ToList();
            ViewData["Conductors"] = _context.conductors.ToList();
            ViewData["Transport"] = _context.transports.ToList();
            ViewData["BusStops"] = _context.busStops.ToList();
            ViewData["TransportRoute"] = _context.transportRoutes.ToList();
            ViewData["Tickets"] = _context.tickets
                .Where(a =>  a.DateOfTrip > DateTime.Now && a.Status ==1)
                .ToList();
            ViewData["Trips"] = _context.trips.ToList();

            return View();
        }

        public async Task<IActionResult> ConfirmTicket(int id)
        {
            var ticket = await _context.tickets.FindAsync(id);
            ticket.Status = 2;  
            if (ticket != null)
            {
                _context.tickets.Update(ticket);
            }
            await _context.SaveChangesAsync();
            return Redirect("~/Dispetcher/ListTickets");
        }

        
    }
}
