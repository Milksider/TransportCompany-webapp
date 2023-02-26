using Microsoft.AspNetCore.Mvc;
using TransportCompany.Data;
using TransportCompany.Models;

namespace TransportCompany.Controllers
{
    public class LoginController : Controller
    {
        private readonly TransportCompanyDbContext _context;
        public LoginController (TransportCompanyDbContext context)
        {
            _context = context;
        }
        public IActionResult Login()
        {
            return View();
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Login([Bind("Email,Password")] Logins logins)
        {
            if (_context.users.Any(a => a.Password == logins.Password && a.Email == logins.Email))
            {
                int id = _context.users.Where(a => a.Password == logins.Password && a.Email == logins.Email).Select(a => a.Id).FirstOrDefault();
                Response.Cookies.Append("userId", id.ToString());
                return RedirectToAction("Index", "User", new { id = id });
            }
            else if (_context.dispetchers.Any(a => a.Password == logins.Password && a.Email == logins.Email))
            {
                int id = _context.dispetchers.Where(a => a.Password == logins.Password && a.Email == logins.Email).Select(a => a.Id).FirstOrDefault();
                Response.Cookies.Append("userId", id.ToString());
                return RedirectToAction("Index", "Dispetcher", new { id = id });
            }
            else if (_context.admins.Any(a => a.Password == logins.Password && a.Email == logins.Email))
            {
                int id = _context.admins.Where(a => a.Password == logins.Password && a.Email == logins.Email).Select(a => a.Id).FirstOrDefault();
                Response.Cookies.Append("userId", id.ToString());
                return RedirectToAction("Index", "Admin", new { id = id });
            }
            return View();
        }
        public IActionResult Logout()
        {
            Response.Cookies.Delete("userId");
            return RedirectToAction("Index","Home");
        }
        [HttpPost]
        public IActionResult Register([Bind("Name,Surname,Fathername,Email,Password")] User user)
        {
            if (user.Name != null)
            {
                _context.users.Add(user);
                _context.SaveChanges();

                int id = _context.users.Where(a => a.Password == user.Password && a.Email == user.Email).Select(a => a.Id).FirstOrDefault();
                Response.Cookies.Append("userId", id.ToString());
                return RedirectToAction("Index", "User", new { id = id });
            }
            else
            {
                return View();
            }
        }
    }
}
