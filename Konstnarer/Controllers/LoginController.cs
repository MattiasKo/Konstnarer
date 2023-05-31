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

            if (email != null || password != null)
            {
                User login = _context.Users.FirstOrDefault(f => f.Email == email);
                if (login == null||login.IsValidated==false)
                {
                    return RedirectToAction("Error", "Home");
                }
                if (login != null && login.IsValidated == true)
                {
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
                        HttpContext.Session.SetString("UserName", userLogin.UserName);
                        HttpContext.Session.SetString("UserId", (userLogin.UserId).ToString());
                        Response.Cookies.Append("AuthId", authId, new Microsoft.AspNetCore.Http.CookieOptions
                        {
                            Expires = DateTime.Now.AddMinutes(10)
                        });
                        Response.Cookies.Append("UserName", userLogin.UserName, new Microsoft.AspNetCore.Http.CookieOptions
                        {
                            Expires = DateTime.Now.AddMinutes(10)
                        });

                        return RedirectToAction("Index","Home");
                    }
                }
                return RedirectToAction("Error", "Home");
            }
            else
            {
                return RedirectToAction("Error", "Home");
            }
        }
        public async Task<IActionResult> ControllPanel(UserLogin login)
        {
            if (Request.Cookies["AuthId"] != null && Request.Cookies["AuthId"] == HttpContext.Session.GetString("AuthId"))
            {
                login.UserName = HttpContext.Session.GetString("UserName");
                login.UserId = Guid.Parse(HttpContext.Session.GetString("UserId"));
                login.IsActive = true;
                ViewData["user"] = login;
            }
            return RedirectToAction("Error", "Home");

        }

        public async Task<IActionResult> Create(UserLogin login)
        {

            UserLogin login1 = new UserLogin();
            if (Request.Cookies["AuthId"] != null && Request.Cookies["AuthId"] == HttpContext.Session.GetString("AuthId"))
            {
                login.UserName = HttpContext.Session.GetString("UserName");
                login.UserId = Guid.Parse(HttpContext.Session.GetString("UserId"));
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
     
        public async Task<IActionResult> Upload(IFormFile files, UserLogin login, bool AllowComments, string Description,string Titel)
        {

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
                            PictureName = Titel,
                            UploadDate = DateTime.Now,
                            Description = Description
                        };
                        _context.Pictures.Add(file);
                        await _context.SaveChangesAsync();
                    }
                    else
                    {
                        ModelState.AddModelError("File", "Filen får inte vara större än 2 MB. Måste vara en bild");
                    }
                }
                return View("UploadComplete");
            }
            return RedirectToAction("Error", "Home");
        }
        public async Task<IActionResult> Logout(UserLogin login)
        {
            HttpContext.Session.Remove("Auth");
            HttpContext.Session.Remove("UserName");
            HttpContext.Session.Remove("UserId");
            Response.Cookies.Delete("AuthId");
            return RedirectToAction("Index", "Home");
        }

        }
    

}
