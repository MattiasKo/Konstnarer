using Konstnarer.Models;
using Konstnarer.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Plugins;

namespace Konstnarer.Controllers
{
    public class ProfileController : Controller
    {
        private readonly AppDbContext _context;

        public ProfileController(AppDbContext Appcontext)
        {
            _context = Appcontext;
        }
        public async Task<IActionResult> Index()
        {
            UserLogin login = new UserLogin();
            if (Request.Cookies["AuthId"] != null && Request.Cookies["AuthId"] == HttpContext.Session.GetString("AuthId"))
            {
                login.UserName = HttpContext.Session.GetString("UserName");
                login.UserId = Guid.Parse(HttpContext.Session.GetString("UserId"));
                login.IsActive = true;
                ViewData["user"] = login;
            }
            else
            {
                login.UserName = "Anonym";
                login.IsActive = false;
                ViewData["user"] = login;
            }

            User user = _context.Users.FirstOrDefault(p => p.UserId == login.UserId);
            List<ProfileComment> comments = _context.ProfileComments.Where(p => p.PersonId == login.UserId).ToList();
            List<Guid> usersCommenting = _context.Users.Select(c => c.UserId).ToList();
            List<User> users = _context.Users.Where(u => usersCommenting.Contains(u.UserId)).ToList();
            List<Picture> pictures = _context.Pictures.Where(p=>p.OwnerId == user.UserId).ToList();

            ViewProfileModel profileModel = new ViewProfileModel()
            {
                UsersPictures = pictures,
                comments = comments,
                ProfileUser = user,

            };


            return View(profileModel);
        }


        // GET: UploadController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UploadController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: UploadController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: UploadController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: UploadController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: UploadController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        [HttpPost]
        public ActionResult Comment(string commentTo, string CommentingUserId,  string usersComment)
        {
            
            ProfileComment comment = new ProfileComment();
            comment.PersonId = Guid.Parse(commentTo);
            comment.UserId = Guid.Parse(CommentingUserId);
            comment.Comment= usersComment;
            _context.ProfileComments.Add(comment);
            _context.SaveChanges();
            return Redirect("Index");
            
        }
    } 
}
