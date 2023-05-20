using Microsoft.AspNetCore.Mvc;

namespace Konstnarer.Controllers
{
    public class SessionController : Controller
    {

        public IActionResult Index()
        {
            string name = HttpContext.Session.GetString("Name");
            return View(model: name);
        }

        public IActionResult SaveName(string name)
        {
            HttpContext.Session.SetString("Name", name);
            return RedirectToAction("Index");
        }
    }
}
