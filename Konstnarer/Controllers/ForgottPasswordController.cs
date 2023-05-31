using Konstnarer.Models;
using Microsoft.AspNetCore.Mvc;

namespace Konstnarer.Controllers
{
    public class ForgottPasswordController : Controller
    {
        private readonly AppDbContext _context;
        public bool once { get; set; }
        public ForgottPasswordController(AppDbContext Appcontext)
        {
            _context = Appcontext;
        }
        public ActionResult Index(string? id)
        {

                    TempData["id"] = id;

                return View();
            }

        [HttpPost]
        public async Task<ActionResult> NewPassword(ChangePassword change, string? id)
        {
            var CPuser = _context.ChangePasswords.FirstOrDefault(c => c.RouteId == id);
            if (CPuser == null)
            {
                return View();
            }
            var user = _context.Users.FirstOrDefault(u => u.UserId == CPuser.UserId);
            if (user == null)
            {
                return View();
            }

            user.Password = change.Password;
            _context.ChangePasswords.Remove(CPuser);
            _context.SaveChanges();

            return RedirectToAction("SetPasswordComplete", "ForgottPassword");
        }
        public IActionResult SetPasswordComplete()
        {
            return View();
        }
    
    }
}
