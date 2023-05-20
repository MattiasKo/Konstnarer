using Konstnarer.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Konstnarer.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index(User user)
        {
            return View(user);
        }

        public IActionResult Privacy()
        {
            return View();
        }


        public IActionResult Error()
        {
            return View();
        }
            
    }
}