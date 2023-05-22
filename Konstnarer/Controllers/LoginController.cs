using Azure.Core;
using Konstnarer.Models;
using Konstnarer.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace Konstnarer.Controllers
{
    public class LoginController : Controller
    {
        private readonly AppDbContext _context;

        public LoginController(AppDbContext Appcontext)
        {
            _context = Appcontext;
        }

        public async Task<IActionResult> Login(string password, string email)
        {

            if (email != null && password != null)
            {
                User login = _context.Users.FirstOrDefault(f => f.Email == email);
                if (login.Password == password)
                {
                    UserLogin userLogin = new()
                    {
                        UserName = login.UserName,
                        UserId = login.UserId,
                        IsActive = true
                    };
                    ViewData["user"] = userLogin;

                    string authId = Guid.NewGuid().ToString();
                    HttpContext.Session.SetString("AuthId", authId);
                    HttpContext.Session.SetString("UserName",userLogin.UserName);
                    Response.Cookies.Append("AuthId",authId);
                    Response.Cookies.Append("UserName", userLogin.UserName);
                    //HttpContext.Session.SetString("UserId", userLogin.UserId.ToString());

                    return View("Index");
                }
                return View();
            }
            else
            {
                return View();
            }
        }
        public async Task<IActionResult> ControllPanel(UserLogin login)
        {
            if (Request.Cookies["AuthId"] == HttpContext.Session.GetString("AuthId"))
            {
                login.UserName = HttpContext.Session.GetString("UserName");
                login.IsActive = true;
                ViewData["user"] = login;
                return View(login);
            }
            return RedirectToAction("Error", "Home");

        }
        public async Task<IActionResult> Index(UserLogin login)
        {
            if (Request.Cookies["AuthId"] == HttpContext.Session.GetString("AuthId"))
            {
                login.UserName = HttpContext.Session.GetString("UserName");
                login.IsActive = true;
                ViewData["user"] = login;
                return View();
            }

            return View();

        }
        public async Task<IActionResult> Create(UserLogin login)
        {
            if (Request.Cookies["AuthId"] == HttpContext.Session.GetString("AuthId"))
            {
                login.UserName = HttpContext.Session.GetString("UserName");
                login.IsActive = true;
                ViewData["user"] = login;
                return View();  
            }
            else
            {
                return RedirectToAction("Error", "Home");
            }
        }
        [HttpPost]
     
        public async Task<IActionResult> Upload(IFormFile files, UserLogin login, bool AllowComments)
        {
            if (Request.Cookies["AuthId"] == HttpContext.Session.GetString("AuthId"))
            {
                login.UserName = HttpContext.Session.GetString("UserName");
                login.IsActive= true;
                ViewData["user"] = login;
            }
            else
            {
                return RedirectToAction("Error", "Home");
            }

            if (files.FileName.EndsWith(".png")|| files.FileName.EndsWith(".jpg")|| files.FileName.EndsWith(".bmp")|| files.FileName.EndsWith(".gif"))
            {
                using (var memoryStream = new MemoryStream())
                {
                    await files.CopyToAsync(memoryStream);
                    // Upload the file if less than 2 MB
                    if (memoryStream.Length < 2097152)
                    {
                        var file = new Picture()
                        {  
                            AllowComments = AllowComments,
                            OwnerId = login.UserId,
                            PicSize = memoryStream.Length,
                            ImageFile = memoryStream.ToArray(),
                            PictureName = files.FileName,
                            UploadDate = DateTime.Now,
                        };
                        _context.Pictures.Add(file);
                        await _context.SaveChangesAsync();
                    }
                    else
                    {
                        ModelState.AddModelError("File", "Filen får inte vara större än 2 MB.");
                    }
                }
                return View("UploadComplete");
            }
            return RedirectToAction("Error", "Home");
        }
        [HttpPost]
        public async Task<IActionResult> Comment(DetailPictureAndComments picComment,int picId)
        {
            Picture picture = _context.Pictures.FirstOrDefault(p => p.Id == picId);
            IEnumerable<PicComment> comments = _context.PicComments.Where(s => s.PictureId == picId);
            if (HttpContext.Session.GetString("UserName") == null)
            {
                return RedirectToAction("Detail","Home", new DetailPictureAndComments()
                {
                    picture = picture,
                    comments = comments,

                });
            }
            User userName = _context.Users.FirstOrDefault(f => f.UserName == HttpContext.Session.GetString("UserName"));
            PicComment userComment = new PicComment();

            userComment.Comment = picComment.usersComment;
            userComment.UserId = userName.UserId;
            userComment.PictureId = picId;

            _context.PicComments.Add(userComment);
            await _context.SaveChangesAsync();

            return RedirectToAction("Detail","Home", new DetailPictureAndComments()
            {
                picture = picture,
                comments = comments,

            });
        }

    }
    

}
