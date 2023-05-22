using Konstnarer.Models;
using Konstnarer.Models.Interfaces;
using Konstnarer.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Plugins;
using System.Collections;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

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

        public async Task<ViewResult> index(UserLogin login)
        {
            if (Request.Cookies["AuthId"] != null && Request.Cookies["AuthId"] == HttpContext.Session.GetString("AuthId"))
            {
                login.IsActive = true;
                ViewData["user"] = login;
            }
            IEnumerable<Picture> pictures;
            pictures = _pictureRepository.GetAllPictures.ToList();

            return View(pictures);
        }
   
        public IActionResult Detail(int picId)
        {
            Picture picture = _context.Pictures.FirstOrDefault(p => p.Id == picId);
            IEnumerable<PicComment> comments = _context.PicComments.Where(s => s.PictureId == picId);
            
            return View("Detail",new DetailPictureAndComments()
            {
                 picture = picture,
                comments = comments,
                
        });
        }

        public IActionResult Error()
        {
            return View();
        }
        public Image byteArrayToImage(byte[] byteArrayIn)
        {
            using (var ms = new MemoryStream(byteArrayIn))
            {
                return Image.FromStream(ms);
            }


        }
        public async Task<IActionResult> Comment(DetailPictureAndComments picComment, int picId)
        {
            Picture picture = _context.Pictures.FirstOrDefault(p => p.Id == picId);
            IEnumerable<PicComment> comments = _context.PicComments.Where(s => s.PictureId == picId);
            if (HttpContext.Session.GetString("UserName") == null)
            {
                return View("Detail",  new DetailPictureAndComments()
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

            return View("Detail",new DetailPictureAndComments()
            {
                picture = picture,
                comments = comments,

            });
        }


    }
}