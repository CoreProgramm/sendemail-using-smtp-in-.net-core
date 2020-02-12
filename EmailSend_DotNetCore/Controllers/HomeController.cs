using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using EmailSend_DotNetCore.Models;
using System.Net.Mail;
using System.Net;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace EmailSend_DotNetCore.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly mailConfig _config;

        public HomeController(ILogger<HomeController> logger, mailConfig config)
        {
            _logger = logger;
            _config = config;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EmailSend(EmailConfig _email)
        {
            using (MailMessage mm = new MailMessage(_config.mailFrom, _email.To))
            {
                mm.Subject = _email.Subject;
                mm.Body = _email.Body;
                if (_email.Attachment.Length > 0)
                {
                    string fileName = Path.GetFileName(_email.Attachment.FileName);
                    mm.Attachments.Add(new Attachment(_email.Attachment.OpenReadStream(), fileName));
                }
                mm.IsBodyHtml = false;
                using (SmtpClient smtp = new SmtpClient())
                {
                    smtp.Host = "smtp.gmail.com";
                    smtp.EnableSsl = true;
                    NetworkCredential NetworkCred = new NetworkCredential(_config.mailFrom, _config.mailpassword);
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = NetworkCred;
                    smtp.Port = 587;
                    smtp.Send(mm);
                    ViewBag.Message = "Email sent.";
                }
            }
            return View("Index");
        }
        



        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
