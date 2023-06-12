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
        public async Task<IActionResult> Index(string userId)
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

            User user = _context.Users.FirstOrDefault(p => p.UserId == Guid.Parse(userId));
            List<ProfileComment> comments = _context.ProfileComments.Where(p => p.PersonId == Guid.Parse(userId)).ToList();
            List<Guid> usersCommenting = _context.Users.Select(c => c.UserId).ToList();
            List<User> users = _context.Users.Where(u => usersCommenting.Contains(u.UserId)).ToList();
            List<Picture> pictures = _context.Pictures.Where(p=>p.OwnerId == user.UserId).ToList();
            ViewProfileModel profileModel = new ViewProfileModel()
            {
                UsersPictures = pictures,
                comments = comments,
                ProfileUser = user,
                UserIdComment = usersCommenting,
                users = users
            };


            return View(profileModel);
        }


        // GET: UploadController/Create
        
        [HttpPost]
        public ActionResult Comment(string commentTo, string CommentingUserId,  string usersComment)
        {
            UserLogin login = new UserLogin();
            if (Request.Cookies["AuthId"] != null && Request.Cookies["AuthId"] == HttpContext.Session.GetString("AuthId"))
            {
            ProfileComment comment = new ProfileComment();
            comment.PersonId = Guid.Parse(commentTo);
            comment.UserId = Guid.Parse(CommentingUserId);
            comment.Comment= usersComment;
            comment.Date = DateTime.Now;
            _context.ProfileComments.Add(comment);
            _context.SaveChanges();
            return RedirectToAction("Index", new {userId = commentTo });
            }
            return View();
        }
       
    } 
}
