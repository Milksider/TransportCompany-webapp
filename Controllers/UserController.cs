using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography.X509Certificates;
using TransportCompany.Data;
using TransportCompany.Models;

namespace TransportCompany.Controllers
{
    public class UserController : Controller
    {
        private readonly TransportCompanyDbContext _context;
        public UserController(TransportCompanyDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            ViewData["User"]= _context.users.Where(a => a.Id == Convert.ToInt32(Request.Cookies["userId"])).ToList();
            return View();
        }
        public IActionResult SuccessCreate()
        {
            return View();
        }
        public IActionResult ChooseRoute()
        {
            ViewData["BusStops"] = _context.busStops.ToList();
            
            return View();
        }
        public IActionResult ChooseTrip([Bind("StartBusStopId,EndBusStopId")] TransportRoute transportRoute )
        {
            string date = Request.Form["date"];
            if (DateTime.Parse(date) < DateTime.Now)
            {
                date = DateTime.Now.ToString();
            }

            int routeId = _context.transportRoutes
                .Where(a => a.StartBusStopId == transportRoute.StartBusStopId && a.EndBusStopId == transportRoute.EndBusStopId)
                .Select(a=>a.Id).FirstOrDefault();
            ViewData["Trips"] = _context.trips.Where(a=>a.RouteId==routeId).ToList();
            ViewData["Drivers"] = _context.drivers.ToList();
            ViewData["Conductors"] = _context.conductors.ToList();
            ViewData["Transport"] = _context.transports.ToList();
            ViewData["BusStopsStart"] = _context.busStops.Where(a=>a.Id==transportRoute.StartBusStopId).ToList();
            ViewData["BusStopsEnd"] = _context.busStops.Where(a=>a.Id == transportRoute.EndBusStopId).ToList();
            ViewData["date"] = date;
            ViewData["TransportSeats"] = _context.tickets.ToList();
            Response.Cookies.Append("RouteId", routeId.ToString());
            Response.Cookies.Append("Date", date.ToString());

            return View();
        }
        public async Task<IActionResult> CreateTicket(int id)
        {
            Ticket ticket = new Ticket();
            ticket.UserId = Convert.ToInt32(Request.Cookies["userId"]);
            ticket.Status = 1;
            ticket.DateOfTrip = Convert.ToDateTime(Request.Cookies["Date"]);
            ticket.TripId = id;
          
            _context.tickets.Add(ticket);
            await _context.SaveChangesAsync();

            return Redirect("~/User/SuccessCreate");
        }
        public IActionResult ListTickets()
        {
            ViewData["Drivers"] = _context.drivers.ToList();
            ViewData["Conductors"] = _context.conductors.ToList();
            ViewData["Transport"] = _context.transports.ToList();
            ViewData["BusStops"] = _context.busStops.ToList();
            ViewData["TransportRoute"] = _context.transportRoutes.ToList();
            ViewData["Tickets"] = _context.tickets
                .Where(a => a.UserId == Convert.ToInt32(Request.Cookies["userId"]))
                .ToList();
            ViewData["Trips"] = _context.trips.ToList();

            return View();
        }
        public async Task<IActionResult> DeleteTicket(int id)
        {
            var ticket = await _context.tickets.FindAsync(id);
            if (ticket != null)
            {
                _context.tickets.Remove(ticket);
            }

            await _context.SaveChangesAsync();
            return Redirect("~/User/ListTickets");
        }

        public IActionResult ListOldTickets()
        {
            ViewData["Drivers"] = _context.drivers.ToList();
            ViewData["Conductors"] = _context.conductors.ToList();
            ViewData["Transport"] = _context.transports.ToList();
            ViewData["BusStops"] = _context.busStops.ToList();
            ViewData["TransportRoute"] = _context.transportRoutes.ToList();
            ViewData["Tickets"] = _context.tickets
                .Where(a => a.UserId == Convert.ToInt32(Request.Cookies["userId"]) && a.DateOfTrip < DateTime.Now && a.Status == 2)
                .ToList();
            ViewData["Trips"] = _context.trips.ToList();
            return View();
        }

    }
}
