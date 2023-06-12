using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;
using System.Net;
using Konstnarer.Models;

namespace Konstnarer.Controllers
{
    public class ValidateController : Controller
    {
        private readonly AppDbContext _context;
        private IConfiguration _configuration;
        public ValidateController(AppDbContext Appcontext, IConfiguration iconfig)
        {
            _context = Appcontext;
            _configuration = iconfig;
        }
        public async Task<ActionResult> Index(string? id)
        {
            var user = _context.ValidateUsers.FirstOrDefault(z => z.RouteId == id);
            if (user != null)
            {
                var ValUser = _context.Users.FirstOrDefault(y => y.UserId == user.UserId);
                if (ValUser != null)
                {
                    //ValUser.Id=ValUser.Id;
                    ValUser.UserName = ValUser.UserName;
                    ValUser.Email = ValUser.Email;
                    ValUser.Password = ValUser.Password;
                    ValUser.FirstName = ValUser.FirstName;
                    ValUser.LastName = ValUser.LastName;
                    ValUser.Role = ValUser.Role;
                    ValUser.UserId = user.UserId;
                    ValUser.IsValidated = true;
                    ValUser.IsActive = true;

                    _context.ValidateUsers.Remove(user);

                    _context.SaveChanges();
                    return View("ValidateComplete");
                }
            }
            return RedirectToAction("Index", "Home");

        }
        public ActionResult ForgotPassword() 
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> RequestNewPassword(string email)
        {
            var user = _context.Users.FirstOrDefault(z => z.Email == email);
            if (user != null)
            {
                ChangePassword CPuser = new()
                {
                    RouteId = Guid.NewGuid().ToString(),
                    UserId = user.UserId,
                    Password = user.Password,
                    ConfirmPassword=user.Password,
                };
                _context.ChangePasswords.Add(CPuser);
                _context.SaveChanges();

                MailMessage message = new System.Net.Mail.MailMessage();
                string fromEmail = _configuration.GetValue<string>("Credential:username");
                string password = _configuration.GetValue<string>("Credential:password");
                string toEmail = email;
                message.From = new MailAddress(fromEmail);
                message.To.Add(toEmail);
                message.Subject = "Byte av lösenord för konstarer.se";
                message.IsBodyHtml = true;
                message.Body = "https://konstnarer-mvc.azurewebsites.net/ForgottPassword?id=" + CPuser.RouteId; ;

                message.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;


                using (SmtpClient smtpClient = new SmtpClient("send.one.com", 587))
                {
                    smtpClient.EnableSsl = true;
                    smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtpClient.UseDefaultCredentials = false;
                    smtpClient.Credentials = new NetworkCredential(fromEmail, password);

                    smtpClient.Send(message.From.ToString(), message.To.ToString(), message.Subject, message.Body);
                }
               return RedirectToAction("PasswordRequestSent","Validate");
            }
            return RedirectToAction("Index", "Home");

        }
       
        public ActionResult ForgottPassword()
        {
            return View();
        }
        public ActionResult PasswordRequestSent()
        {
            return View();
        }
    }
}
