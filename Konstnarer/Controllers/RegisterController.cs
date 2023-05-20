using Konstnarer.Models;
using Konstnarer.Models.Register;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Konstnarer.Controllers
{
    public class RegisterController : Controller
    {
        private readonly AppDbContext _context;
        public RegisterController(AppDbContext Appcontext)
        {
            _context = Appcontext;
        }
        // GET: RegisterController


        // GET: RegisterController/Details/5
        //public ActionResult Details(int id)
        //{
        //    return View();
        //}

        public ActionResult Index()
        {
            
            return View();
        }
        // GET: RegisterController/Create

        public ActionResult RegisterComplete(User user)
        {
           
            return View(user);
        }

        // POST: RegisterController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Index(RegisterModel regModel)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            User user = new()
            {
                UserName = regModel.UserName,
                Email = regModel.Email,
                Password = regModel.Password,
                FirstName = regModel.FirstName,
                LastName = regModel.LastName,
                Role = "User",
                IsActive = false,
                UserId = Guid.NewGuid(),
                
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return RedirectToAction("RegisterComplete",regModel);

        }

        // GET: RegisterController/Edit/5
        //public ActionResult Edit(int id)
        //{
        //    return View();
        //}

        //// POST: RegisterController/Edit/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        //// GET: RegisterController/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        //// POST: RegisterController/Delete/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Delete(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
    }
}
