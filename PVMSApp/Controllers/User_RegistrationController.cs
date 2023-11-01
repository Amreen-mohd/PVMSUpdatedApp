using Newtonsoft.Json.Linq;
using PVMSApp.Models.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace PVMSApp.Controllers
{
    public class User_RegistrationController : Controller
    {
        // GET: User_Registration
        public ActionResult Index()
        {
            List<string> qlist = new List<string>();
            string q = "what is your pet name";
            string q1 = "what is your favourite holiday spot";
            string q3 = "who is your favourite actor";
            string q4 = "what is your nationality";
            qlist.Add(q1);
            qlist.Add(q3);
            qlist.Add(q4);
            qlist.Add(q);
            ViewBag.qlist = qlist;
            return View();
        }

        public ActionResult Demo()
        {
            List<string> qlist = new List<string>();
            string q = "what is your pet name";
            string q1 = "what is your favourite holiday spot";
            string q3 = "who is your favourite actor";
            string q4 = "what is your nationality";
            qlist.Add(q1);
            qlist.Add(q3);
            qlist.Add(q4);
            qlist.Add(q);
            ViewBag.qlist = qlist;
            return View();
        }

        [HttpPost]
        public ActionResult Demo(user_registration2 user)
        {
            List<string> qlist = new List<string>();
            string q = "what is your pet name";
            string q1 = "what is your favourite holiday spot";
            string q3 = "who is your favourite actor";
            string q4 = "what is your nationality";
            qlist.Add(q1);
            qlist.Add(q3);
            qlist.Add(q4);
            qlist.Add(q);
            ViewBag.qlist = qlist;
            if (ModelState.IsValid)
            {
                user_registration2 u = new DbOperations().insert(user);

                TempData["flag"] = "true";

                TempData["UserID"] = u.userId;
                TempData["Password"] = u.password;

                return RedirectToAction("Login", new { success = true });
            }

            return View();
        }

        
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string userId, string contactNo, string password)
        {
            //Validate Google recaptcha here
            var response = Request["g-recaptcha-response"];
            string secretKey = "6Lc3E6onAAAAALlGobkwuUq2yMiWZp0vq_6PHi3z";
            var client = new WebClient();
            var result = client.DownloadString(string.Format("https://www.google.com/recaptcha/api/siteverify?secret={0}&response={1}", secretKey, response));
            var obj = JObject.Parse(result);
            var status = (bool)obj.SelectToken("success");
            ViewBag.Message = status ? "Google reCaptcha validation success" : "Google reCaptcha validation failed";
            if (new DbOperations().checkUser(userId, contactNo, password))
            {
                HttpCookie c1 = new HttpCookie("userId");
                c1.Value= userId;
                Response.Cookies.Add(c1);
                Session["userID"] = userId;
                return RedirectToAction("Index","Services");
            }

            ViewBag.msg = "* Invalid Credentials";
            return View();
        }

        public ActionResult Check(string userId, string contactNo, string password)
        {
            if (new DbOperations().checkUser(userId, contactNo, password))
            {
                HttpCookie c1 = new HttpCookie("userId");
                c1.Value = userId;
                Response.Cookies.Add(c1);

                
                return Json("Success");
            }

            return Json("Invalid Credentials");
        }

        public ActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ForgotPassword(string email)
        {
            return RedirectToAction("sendEmail", "Email", email);
        }
    }
}