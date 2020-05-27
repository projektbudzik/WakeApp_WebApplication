using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WakeApp.Models;

namespace WakeApp.Controllers
{
    public class ContactController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Index(ContactViewModel mailbody)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    MailMessage message = new MailMessage();
                    message.From = new MailAddress(mailbody.Email); //Email which is get from contact us page 
                    message.To.Add("projektbudzik2020@gmail.com");  //Where mail will be sent 
                    message.Subject = mailbody.Subject;
                    
                    string messageBody = "Witaj,";
                    messageBody += "<br />masz nową prośbę o kontakt z serwisu WakeApp:";
                    messageBody += "<br /><br /><b>Imię:</b> " + mailbody.Name;
                    messageBody += "<br /><b>Email:</b> " + mailbody.Email;
                    messageBody += "<br /><b>Telefon:</b> " + mailbody.Phone;
                    messageBody += "<br /><br /><b>Temat:</b> " + mailbody.Subject;
                    messageBody += "<br /><b>Treść Wiadomości:</b>";
                    messageBody += "<br />"+ mailbody.Message;
                    message.Body = messageBody;
                    message.IsBodyHtml = true;

                    SmtpClient smtp = new SmtpClient(); //SMTP Class Properties                    
                    smtp.Host = "smtp.webio.pl";        //SMTP Server
                    smtp.Port = 587;                    //Port Number of the SMTP server (Gmail: 587)
                    smtp.EnableSsl = true;              //Specify whether your host accepts SSL Connections
                    smtp.UseDefaultCredentials = false;  //Set to True in order to allow authentication based on the Credentials of the Account used to send emails
                    smtp.Credentials = new NetworkCredential("biuro@projektbudzik.pl", "http://projektbudzik.pl/1");   //Valid login credentials for the SMTP server (Gmail: email address and password)

                    smtp.Send(message);


                    ModelState.Clear();
                    ViewBag.Message = "Dziękujemy za kontakt. Odpiszemy najszybciej jak to będzie możliwe.";
                }
                catch (Exception ex)
                {
                    ModelState.Clear();
                    ViewBag.Message = $" Przepraszamy, coś poszło nie tak... {ex.Message}";
                }
            }

            return View();
        }
        public IActionResult Error()
        {
            return View();
        }
    }
}
