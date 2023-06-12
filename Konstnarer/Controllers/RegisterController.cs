using Konstnarer.Models;
using Konstnarer.Models.Register;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using NuGet.Protocol.Plugins;
using System.Configuration;
using System.Net;
using System.Net.Mail;
using System.Web.Helpers;

namespace Konstnarer.Controllers
{
    public class RegisterController : Controller
    {
        private readonly AppDbContext _context;
        private IConfiguration _configuration;
        public RegisterController(AppDbContext Appcontext, IConfiguration iconfig)
        {
            _context = Appcontext;
            _configuration = iconfig;
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
                IsValidated = false
            };
            ValidateUser ValUser = new()
            {
                RouteId = Guid.NewGuid().ToString(),
                UserId = user.UserId,
            };
            User UserExists = _context.Users.FirstOrDefault(x => x.Email == user.Email);
            if (UserExists != null)
            {
                ModelState.AddModelError("Email", "e-posten används redan.");
                return View(regModel);
            }
            _context.ValidateUsers.Add(ValUser);
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            MailMessage message = new System.Net.Mail.MailMessage();
            string fromEmail = _configuration.GetValue<string>("Credential:username");
            string password = _configuration.GetValue<string>("Credential:password");
            string toEmail = regModel.Email;
            message.From = new MailAddress(fromEmail);
            message.To.Add(toEmail);
            message.Subject = "Validera e-post för konstarer.se";
            message.IsBodyHtml = true;
            message.Body = "https://konstnarer-mvc.azurewebsites.net/Validate?id=" + ValUser.RouteId; ;
                
            message.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
         

            using (SmtpClient smtpClient = new SmtpClient("send.one.com", 587))
            {
                smtpClient.EnableSsl = true;
                smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new NetworkCredential(fromEmail, password);
               
                smtpClient.Send(message.From.ToString(), message.To.ToString(), message.Subject, message.Body);
            }


            return RedirectToAction("RegisterComplete");

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
