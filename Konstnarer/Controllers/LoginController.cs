using Konstnarer.Models;
using Microsoft.AspNetCore.Mvc;

namespace Konstnarer.Controllers
{
    public class LoginController : Controller
    {
        private readonly AppDbContext _context;
        public LoginController(AppDbContext Appcontext)
        {
            _context = Appcontext;
        }

        public async Task<IActionResult> Login(User user)
        {
            if (user.Email != null && user.Password != null)
            {
                User login = _context.Users.FirstOrDefault(f => f.Email == user.Email);
                if (login.Password == user.Password)
                {
                    login.IsActive = true;
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index", "Login",login);
                }
                return View();
            }
            else
            {
                return View();
            }
        }
        public async Task<IActionResult> ControllPanel(User login)
        {
 
                return View(login);
            
        }
        public async Task<IActionResult> Index(User login)
        {

            return View(login);

        }

    }
}
