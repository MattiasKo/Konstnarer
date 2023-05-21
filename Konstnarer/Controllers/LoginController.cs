using Azure.Core;
using Konstnarer.Models;
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
                    Response.Cookies.Append("AuthId",authId);
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
                return View(login);
            }
            return RedirectToAction("Error", "Home");

        }
        public async Task<IActionResult> Index()
        {

            return View();

        }
        public async Task<IActionResult> Create()
        {
            Guid userid = Guid.Parse(HttpContext.Session.GetString("UserId"));
            User user = _context.Users.FirstOrDefault(s => s.UserId == userid);

            if (user.UserId == userid)
            {
                UserLogin userLogin = new()
                {
                    UserName = user.UserName,
                    UserId = user.UserId,
                    IsActive = true
                };
                ViewData["user"] = userLogin;
                return View();
                
            }
            else
            {
                return RedirectToAction("Error", "Home");
            }
        }
        [HttpPost]
     
        public async Task<IActionResult> Upload(IFormFile files)
        {
            Guid userid = Guid.Parse(HttpContext.Session.GetString("UserId"));
            User user = _context.Users.FirstOrDefault(s => s.UserId == userid);

            if (user.UserId == userid)
            {
                UserLogin userLogin = new()
                {
                    UserName = user.UserName,
                    UserId = user.UserId,
                    IsActive = true
                };
                ViewData["user"] = userLogin;
            }
            else
            {
                return RedirectToAction("Error", "Home");
            }
            if (files != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await files.CopyToAsync(memoryStream);
                    // Upload the file if less than 2 MB
                    if (memoryStream.Length < 2097152)
                    {
                        var file = new Picture()
                        {  
                            AllowComments = true,
                            OwnerId = user.UserId,
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

        }

}
