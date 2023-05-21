using Konstnarer.Models;
using Konstnarer.Models.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        public async Task<ViewResult> index()
        {
            IEnumerable<Picture> pictures;
            pictures = _pictureRepository.GetAllPictures.ToList();

            return View(pictures);
        }
        //public async Task<ViewResult> Index()
        //{
        //    IEnumerable<Picture> pictures;
        //    pictures = _pictureRepository.GetAllPictures.ToList();

        //    List<Picture> ConPictures = new List<Picture>();

        //    foreach (Picture pic in pictures)
        //    {
        //        Picture converted = new Picture();
        //        converted.PictureName = pic.PictureName;
        //        converted.UploadDate = pic.UploadDate;
        //        converted.OwnerId = pic.OwnerId;
        //        converted.PicComments = pic.PicComments;
        //        converted.AllowComments = pic.AllowComments;
        //        converted.PathName = pic.PathName;
        //        using (Image image = Image.FromStream(new MemoryStream(pic.ImageFile)))
        //        {
        //            image.Save(@"wwwroot/images/"+pic.PictureName, ImageFormat.Png);  // Or Png
        //            converted.NewImage = image;
        //        }
        //       //converted.NewImage = byteArrayToImage(pic.ImageFile);
        //        ConPictures.Add(converted);
        //    }
            

        //    return View(ConPictures);
        //}

        public IActionResult Privacy()
        {
            return View();
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
        

    }
}