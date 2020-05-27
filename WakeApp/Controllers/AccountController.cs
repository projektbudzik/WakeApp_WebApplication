using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using WakeApp.Model;
using WakeApp.Models;

namespace WakeApp.Controllers
{
    public class AccountController : BaseController
    {
        public AccountController(WakeAppContext wakeAppContext): base(wakeAppContext)
        { }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel loginModel)
        {
            var user = wakeAppContext.User.FirstOrDefault(user => user.Email == loginModel.Email);
            if (user == null)
            {
                ModelState.TryAddModelError("Email", "Błędny adres email");
            }
            else if (user.Password != ComputeHash(loginModel.Password, user.Salt))
            {
                ModelState.TryAddModelError("Password", "Błędne hasło");
            }

            if (!ModelState.IsValid)
            {
                return View(loginModel);
            }

            await SignIn(user);

            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View(new RegisterViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel registerModel)
        {
   
            if (wakeAppContext.User.Any(user => user.Email == registerModel.Email))
            {
                ModelState.TryAddModelError("Email", "Podany adres email już istnieje");
            }

            var group = wakeAppContext
                    .Group
                    .FirstOrDefault(g => g.Name.ToLower() == registerModel.GroupName.ToLower());

            if (registerModel.CreateNewGroup)
            {
                if (group != null)
                {
                    ModelState.TryAddModelError("GroupName", "Podana nazwa grupy już istnieje");
                }
                if (registerModel.GroupPassword != registerModel.GroupConfirmPassword)
                {
                    ModelState.TryAddModelError("GroupPassword", "The group password and confirmation password do not match");
                }  
            }
            else
            {
                if (group == null)
                {
                    ModelState.TryAddModelError("GroupName", "Nie znaleziono grupy o takiej nazwie");
                }
                else if (group.Password != ComputeHash(registerModel.GroupPassword, group.Salt))
                {
                    ModelState.TryAddModelError("GroupPassword", "Błędne hasło grupy");
                }
            }

            if (!ModelState.IsValid)
            {
                return View(registerModel);
            }
            else
            {
                // Sending Email message to 
                using (MailMessage message = new MailMessage("biuro@projektbudzik.pl", registerModel.Email))
                {
                    message.Subject = "WakeApp - potwierdzenie utworzenia konta";
                    string messageBody = "Witaj " + registerModel.Name + "!";
                    messageBody += "<br /><b>Gratulacje!</b> Właśnie utworzyłeś/aś konto w serwisie <b>WakeApp</b>.";
                    messageBody += "<br />Dziękujemy za rejestrację.";
                    messageBody += "<br /><br />Pozdrawiamy";
                    messageBody += "<br />Zespół WakeApp";
                    message.Body = messageBody;
                    message.IsBodyHtml = true;
                                       
                    SmtpClient smtp = new SmtpClient(); //SMTP Class Properties                    
                    smtp.Host = "smtp.webio.pl";        //SMTP Server
                    smtp.Port = 587;                    //Port Number of the SMTP server (Gmail: 587)
                    smtp.EnableSsl = true;              //Specify whether your host accepts SSL Connections
                    smtp.UseDefaultCredentials = false;  //Set to True in order to allow authentication based on the Credentials of the Account used to send emails
                    smtp.Credentials = new NetworkCredential("biuro@projektbudzik.pl", "http://projektbudzik.pl/1");   //Valid login credentials for the SMTP server (Gmail: email address and password)

                    smtp.Send(message);
                }
            }

            // Add group
            if (registerModel.CreateNewGroup)
            {
                string groupSalt = GenerateSalt();
                var newGroup = new Group()
                {
                    Name = registerModel.GroupName,
                    Salt = groupSalt,
                    Password = ComputeHash(registerModel.GroupPassword, groupSalt),
                };
                wakeAppContext.Group.Add(newGroup);
                group = newGroup;
            }

            // Add user
            string userSalt = GenerateSalt();
            var user = new User()
            {
                Name = registerModel.Name,
                Email = registerModel.Email,
                Password = ComputeHash(registerModel.Password, userSalt),
                Salt = userSalt,
                UserRole = registerModel.CreateNewGroup ? "SuperUser" : "User",
                Group = group

            };
            wakeAppContext.User.Add(user);
            wakeAppContext.SaveChanges();

            await this.SignIn(user);

            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

        private async Task SignIn(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                new Claim(ClaimTypes.GroupSid, user.GroupId.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim("Name", user.Name),
                new Claim(ClaimTypes.Role, user.UserRole),
            };

            var claimsIdentity = new ClaimsIdentity(
                claims, CookieAuthenticationDefaults.AuthenticationScheme);


            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                new AuthenticationProperties());
        }

        private string GenerateSalt()
        {
            return Path.GetRandomFileName().Substring(0, 10); 
        }

        private string ComputeHash(string password, string salt)
        {
            using (SHA1Managed sha1 = new SHA1Managed())
            {
                byte[] hash = sha1.ComputeHash(Encoding.UTF8.GetBytes(password + salt));

                byte[] hashWithSalt = hash.Concat(Encoding.UTF8.GetBytes(salt)).ToArray();

                return Convert.ToBase64String(hashWithSalt);
            }
        }
    }
}
