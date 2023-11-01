using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MailKit.Net.Smtp;
using PVMSApp.Models.DAO;
using System.Net;

namespace PVMSApp.Controllers
{
    public class EmailController : Controller
    {
        // GET: Email
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult SendEmail(string email)
        {
            try
            {
                user_registration2 user = new DbOperations().fetchPassword(email);
                MimeMessage msg = new MimeMessage();
                msg.From.Add(new MailboxAddress("sai", "kadarisaikrishna112@gmail.com"));
                msg.To.Add(MailboxAddress.Parse(email));
                msg.Subject = "Hello";
                msg.Body = new TextPart("plain")
                {
                    Text = "Hello " + user.firstName + "Your password is : " + user.password
                };

                SmtpClient smtp = new SmtpClient();
                smtp.Connect("smtp.gmail.com", 465, true);
                smtp.Authenticate("kadarisaikrishna112@gmail.com", "qqyrplhmnhuqkuza");
                smtp.Send(msg);
                smtp.Disconnect(true);
                smtp.Dispose();
                return Json(HttpStatusCode.OK);
            }
            catch(Exception ex)
            {
                return Json(HttpStatusCode.InternalServerError);
            }
        }
    }
}