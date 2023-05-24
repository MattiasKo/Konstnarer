using Konstnarer.Models;
using Konstnarer.Models.Interfaces;
using Konstnarer.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Plugins;

namespace Konstnarer.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IPictureRepository _pictureRepository;
        public HomeController(AppDbContext Appcontext, IPictureRepository pictureRepository)
        {
            _context = Appcontext;
            _pictureRepository = pictureRepository;
        }

        public async Task<ViewResult> Index(UserLogin login)
        {
            if (Request.Cookies["AuthId"] != null && Request.Cookies["AuthId"] == HttpContext.Session.GetString("AuthId"))
            {
                login.UserName = HttpContext.Session.GetString("UserName");
                login.UserId = Guid.Parse(HttpContext.Session.GetString("UserId"));
                login.IsActive = true;
                    ViewData["user"] = login;
            }
            IEnumerable<Picture> pictures;
            pictures = _pictureRepository.GetAllPictures.ToList();

            return View(pictures);
        }
   
        public IActionResult Detail(int picId)
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
            Picture picture = _context.Pictures.FirstOrDefault(p => p.Id == picId);
            List<PicComment> picComments = _context.PicComments.Where(pc => pc.PictureId == picId).ToList();
            List<Guid> userIds = picComments.Select(pc => pc.UserId).ToList();
            List<User> users = _context.Users.Where(u=>userIds.Contains(u.UserId)).ToList();
            DetailPictureAndComments viewModel = new DetailPictureAndComments()
            {
                Pictures = picture,
                pictureComments = picComments,
                Users = users

            };
            
            return View(viewModel);
        }

        public IActionResult Error()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Comment(string usersComment, int picId)
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


            //Picture picture = _context.Pictures.FirstOrDefault(p => p.Id == picId);
            //List<PicComment> picComments = _context.PicComments.Where(pc => pc.PictureId == picId).ToList();
            //List<User> userName = _context.Users.Where(f => f.UserName == HttpContext.Session.GetString("UserName")).ToList();
            //User userIdCom = _context.Users.FirstOrDefault(u => u.UserName == HttpContext.Session.GetString("UserName"));
            //PicComment userComment = new PicComment();


            PicComment comment = new PicComment();
            comment.PictureId = picId;
            comment.UserId = login.UserId;
            comment.Comment = usersComment;
            _context.PicComments.Add(comment);
            _context.SaveChanges();

            return RedirectToAction("Detail","Home",new {picId = picId});

            //userComment.Comment = picComment.usersComment;
            //userComment.UserId = userIdCom.UserId;
            //userComment.PictureId = picId;

            //_context.PicComments.Add(userComment);
            //await _context.SaveChangesAsync();

            //return View("Detail",new DetailPictureAndComments()
            //{
            //    Pictures = picture,
            //    pictureComments = picComments,
            //    Users = userName

            //});
        }


    }
}