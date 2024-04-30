using BloggingPlatform2.Data;
using BloggingPlatform2.Models.Domain;
using Microsoft.AspNetCore.Mvc;

namespace BloggingPlatform2.Controllers
{
    public class AccountController : Controller
    {

        private readonly BloggerDbContext _context;

        public AccountController(BloggerDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        
        public IActionResult Login(User model)
        {
            
                var user = _context.Users.FirstOrDefault(u => u.Username.Equals(model.Username) && u.Password.Equals(model.Password));
                if (user != null)
                {
  
                HttpContext.Session.SetInt32("ID", user.Id);
                HttpContext.Session.SetString("UName", user.Username);
                HttpContext.Session.SetString("URole", user.Role);

                TempData["LoginSuccess"] = true;
                return RedirectToAction("Index", "Home");
                }
            ViewBag.Notification = "Invalid login attempt ";
            
            return View(model);
        }

        [HttpGet]
        public IActionResult Register()
        {
           
            return View();
        }

        [HttpPost]
  
        public IActionResult Register(User model)
        {
           

                if (_context.Users.Any(u => u.Username == model.Username))
                {
                ViewBag.Notification = "This account has already existed, Please Login";
                    return View();
                }
            else { 

                model.Role = "User";
                _context.Users.Add(model);
                _context.SaveChanges();

                var user = _context.Users.FirstOrDefault(u => u.Username.Equals(model.Username) && u.Password.Equals(model.Password));
                if (user != null)
                {

                    HttpContext.Session.SetInt32("ID", user.Id);
                    HttpContext.Session.SetString("UName", user.Username);
                    HttpContext.Session.SetString("URole", user.Role);
                }
                    TempData["RegistrationSuccess"] = true;
                    return RedirectToAction("Index", "Home");
                

            }

            
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> AccessDenied()
        {
            return View();
        }

    }
}