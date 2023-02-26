using Microsoft.AspNetCore.Mvc;
using System.Net.Sockets;
using TransportCompany.Data;
using TransportCompany.Models;

namespace TransportCompany.Controllers
{
    public class AdminController : Controller
    {
        private readonly TransportCompanyDbContext _context;

        public AdminController(TransportCompanyDbContext context)
        {
            _context = context;
        }


        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ListTrips()
        {
            ViewData["Trips"] = _context.trips.ToList();
            ViewData["Drivers"] = _context.drivers.ToList();
            ViewData["Conductors"] = _context.conductors.ToList();
            ViewData["Transport"] = _context.transports.ToList();
            ViewData["Routes"] = _context.transportRoutes.ToList();
            ViewData["BusStops"] = _context.busStops.ToList();
            return View();
        }

        public async Task<IActionResult> DeleteTrip(int id)
        {
            var trip = await _context.trips.FindAsync(id);
            if (trip != null)
            {
                _context.trips.Remove(trip);
            }

            await _context.SaveChangesAsync();
            return Redirect("~/Admin/ListTrips");
        }

        public IActionResult ChangeDriver(int id)
        {
            ViewData["Trips"] = _context.trips.Where(a=>a.Id==id).ToList();
            ViewData["Drivers"] = _context.drivers.ToList();
            Response.Cookies.Append("id", id.ToString());
            return View();
        }

        public IActionResult SaveDriver([Bind("DriverId")] Trip trip)
        {
            trip.Id = Convert.ToInt32(Request.Cookies["id"]);
            trip.ConductorId = _context.trips.Where(a => a.Id == trip.Id).Select(a => a.ConductorId).FirstOrDefault();
            trip.TransportId = _context.trips.Where(a => a.Id == trip.Id).Select(a => a.TransportId).FirstOrDefault();
            trip.TripTime = _context.trips.Where(a => a.Id == trip.Id).Select(a => a.TripTime).FirstOrDefault();
            trip.RouteId = _context.trips.Where(a => a.Id == trip.Id).Select(a => a.RouteId).FirstOrDefault();
            Response.Cookies.Delete("id");
            _context.trips.Update(trip);

            _context.SaveChanges();

            return Redirect("~/Admin/ListTrips");
        }

        public IActionResult ChangeConductor(int id)
        {
            ViewData["Trips"] = _context.trips.Where(a => a.Id == id).ToList();
            ViewData["Conductors"] = _context.conductors.ToList();
            Response.Cookies.Append("id", id.ToString());
            return View();
        }

        public IActionResult SaveConductor([Bind("ConductorId")] Trip trip)
        {
            trip.Id = Convert.ToInt32(Request.Cookies["id"]);
            trip.DriverId = _context.trips.Where(a => a.Id == trip.Id).Select(a => a.DriverId).FirstOrDefault();
            trip.TransportId = _context.trips.Where(a => a.Id == trip.Id).Select(a => a.TransportId).FirstOrDefault();
            trip.TripTime = _context.trips.Where(a => a.Id == trip.Id).Select(a => a.TripTime).FirstOrDefault();
            trip.RouteId = _context.trips.Where(a => a.Id == trip.Id).Select(a => a.RouteId).FirstOrDefault();
            Response.Cookies.Delete("id");
            _context.trips.Update(trip);

            _context.SaveChanges();

            return Redirect("~/Admin/ListTrips");
        }

        public IActionResult ChangeTransport(int id)
        {
            ViewData["Trips"] = _context.trips.Where(a => a.Id == id).ToList();
            ViewData["Transport"] = _context.transports.ToList();
            Response.Cookies.Append("id", id.ToString());
            return View();
        }

        public IActionResult SaveTransport([Bind("TransportId")] Trip trip)
        {
            trip.Id = Convert.ToInt32(Request.Cookies["id"]);
            trip.DriverId = _context.trips.Where(a => a.Id == trip.Id).Select(a => a.DriverId).FirstOrDefault();
            trip.ConductorId = _context.trips.Where(a => a.Id == trip.Id).Select(a => a.ConductorId).FirstOrDefault();
            trip.TripTime = _context.trips.Where(a => a.Id == trip.Id).Select(a => a.TripTime).FirstOrDefault();
            trip.RouteId = _context.trips.Where(a => a.Id == trip.Id).Select(a => a.RouteId).FirstOrDefault();
            Response.Cookies.Delete("id");
            _context.trips.Update(trip);

            _context.SaveChanges();

            return Redirect("~/Admin/ListTrips");
        }

        public IActionResult CreateTrip()
        {
            ViewData["Transport"] = _context.transports.ToList();
            ViewData["Conductors"] = _context.conductors.ToList();
            ViewData["Drivers"] = _context.drivers.ToList();
            ViewData["Routes"] = _context.transportRoutes.ToList();
            ViewData["BusStops"] = _context.busStops.ToList();
            return View();
        }

        public async Task<IActionResult> SaveCreatedTrip([Bind("TransportId,ConductorId,DriverId,TripTime,RouteId")] Trip trip)
        {
            _context.trips.Add(trip);
            await _context.SaveChangesAsync();

            return Redirect("~/Admin/ListTrips");
        }

        public IActionResult ListRoutes()
        {
            ViewData["Routes"] = _context.transportRoutes.ToList();
            ViewData["BusStops"] = _context.busStops.ToList();
            return View();
        }

        public async Task<IActionResult> DeleteRoute(int id)
        {
            var routes = await _context.transportRoutes.FindAsync(id);
            if (routes != null)
            {
                _context.transportRoutes.Remove(routes);
            }

            await _context.SaveChangesAsync();
            return Redirect("~/Admin/ListRoutes");
        }

        public IActionResult CreateRoute()
        {
            ViewData["Routes"] = _context.transportRoutes.ToList();
            ViewData["BusStops"] = _context.busStops.ToList();
            return View();
        }

        public async Task<IActionResult> SaveCreatedRoute([Bind("StartBusStopId,EndBusStopId")] TransportRoute transportRoute)
        {
            _context.transportRoutes.Add(transportRoute);
            await _context.SaveChangesAsync();

            return Redirect("~/Admin/ListRoutes");
        }

        public IActionResult ListBusStops()
        {
            ViewData["BusStops"] = _context.busStops.ToList();
            return View();
        }

        public async Task<IActionResult> DeleteBusStop(int id)
        {
            var busStop = await _context.busStops.FindAsync(id);
            if (busStop != null)
            {
                _context.busStops.Remove(busStop);
            }

            await _context.SaveChangesAsync();
            return Redirect("~/Admin/ListBusStops");
        }

        public IActionResult CreateBusStop()
        {
            return View();
        }

        public async Task<IActionResult> SaveCreatedBusStop([Bind("Name")] BusStop busStop)
        {
            _context.busStops.Add(busStop);
            await _context.SaveChangesAsync();

            return Redirect("~/Admin/ListBusStops");
        }

        public IActionResult ListDrivers()
        {
            ViewData["Drivers"] = _context.drivers.ToList();
            return View();
        }

        public async Task<IActionResult> DeleteDrivers(int id)
        {
            var driver = await _context.drivers.FindAsync(id);
            if (driver != null)
            {
                _context.drivers.Remove(driver);
            }

            await _context.SaveChangesAsync();
            return Redirect("~/Admin/ListDrivers");
        }

        public IActionResult CreateDrivers()
        {

            return View();
        }

        public async Task<IActionResult> SaveCreatedDrivers([Bind("Name,Surname,Fathername")] Driver driver)
        {
            _context.drivers.Add(driver);
            await _context.SaveChangesAsync();

            return Redirect("~/Admin/ListDrivers");
        }

        public IActionResult ListConductors()
        {
            ViewData["Conductors"] = _context.conductors.ToList();
            return View();
        }
        public async Task<IActionResult> DeleteConductors(int id)
        {
            var conductor = await _context.conductors.FindAsync(id);
            if (conductor != null)
            {
                _context.conductors.Remove(conductor);
            }

            await _context.SaveChangesAsync();
            return Redirect("~/Admin/ListConductors");
        }

        public IActionResult CreateConductors()
        {

            return View();
        }

        public async Task<IActionResult> SaveCreatedConductors([Bind("Name,Surname,Fathername")] Conductor conductor)
        {
            _context.conductors.Add(conductor);
            await _context.SaveChangesAsync();

            return Redirect("~/Admin/ListConductors");
        }

        public IActionResult ListTransports()
        {
            ViewData["Transport"] = _context.transports.ToList();
            return View();
        }

        public async Task<IActionResult> DeleteTransports(int id)
        {
            var transport = await _context.transports.FindAsync(id);
            if (transport != null)
            {
                _context.transports.Remove(transport);
            }

            await _context.SaveChangesAsync();
            return Redirect("~/Admin/ListTransports");
        }

        public IActionResult CreateTransports()
        {

            return View();
        }

        public async Task<IActionResult> SaveCreatedTransports([Bind("Name,AutoNumber,NumberOfSeats")] Transport transport)
        {
            _context.transports.Add(transport);
            await _context.SaveChangesAsync();

            return Redirect("~/Admin/ListTransports");
        }

        public IActionResult ListDispetchers()
        {
            ViewData["Dispetchers"] = _context.dispetchers.ToList();
            return View();
        }

        public async Task<IActionResult> DeleteDispetchers(int id)
        {
            var dispetcher = await _context.dispetchers.FindAsync(id);
            if (dispetcher != null)
            {
                _context.dispetchers.Remove(dispetcher);
            }

            await _context.SaveChangesAsync();
            return Redirect("~/Admin/ListDispetchers");
        }

        public IActionResult CreateDispetchers()
        {

            return View();
        }

        public async Task<IActionResult> SaveCreatedDispetchers([Bind("Name,Surname,Fathername,Email,Password")] Dispetcher dispetcher)
        {
            _context.dispetchers.Add(dispetcher);
            await _context.SaveChangesAsync();

            return Redirect("~/Admin/ListDispetchers");
        }

    }
}
